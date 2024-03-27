using System.Collections.ObjectModel;
using System.Composition;
using System.Reactive;
using System.Reactive.Linq;
using Asv.Common;
using Asv.Drones.Gui.Api;
using Asv.Mavlink;
using Avalonia.Controls;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Asv.Drones.Gui.Plugin.Sdr;

[Export]
public class SdrPayloadBrowserViewModel : ViewModelBase
{
    private readonly ILocalizationService _loc;
    private readonly ILogService _log;
    private readonly ReadOnlyObservableCollection<SdrDeviceViewModel> _devices;

    public SdrPayloadBrowserViewModel() : base(SdrWellKnownUri.SdrDeviceBrowser)
    {
        if (Design.IsDesignMode)
        {
            _devices = new ReadOnlyObservableCollection<SdrDeviceViewModel>(
                new ObservableCollection<SdrDeviceViewModel>(new List<SdrDeviceViewModel>
                {
                    new(110),
                    new(120),
                    new(130),
                }));
        }
    }

    public SdrPayloadBrowserViewModel(IMavlinkDevicesService mavlink, ILocalizationService loc, ILogService log) :
        this()
    {
        _loc = loc;
        _log = log;
        
        mavlink
            .Payloads
            .Transform(_ => new SdrDeviceViewModel(_, loc, log))
            .Bind(out _devices)
            .DisposeMany()
            .Subscribe()
            .DisposeItWith(Disposable);
        
        SelectedDevice = _devices.FirstOrDefault();
        
        this.WhenValueChanged(_ => SelectedDevice)
            .Where(_ => _ != null)
            .Subscribe(_ => _.DownloadRecords.Execute().Subscribe())
            .DisposeItWith(Disposable);

        this.WhenAnyValue(_ => _.SelectedDevice)
            .Subscribe(_ => { IsAnySelected = _ != null; })
            .DisposeItWith(Disposable);
    }

    public ReactiveCommand<Unit, Unit> Refresh { get; set; }

    public ReadOnlyObservableCollection<SdrDeviceViewModel> Devices => _devices;

    [Reactive] public SdrDeviceViewModel? SelectedDevice { get; set; }

    [Reactive] public bool IsAnySelected { get; set; }

    public void TrySelect(Guid recordId)
    {
        SelectedDevice?.TrySelect(recordId);
    }
}

public class SdrDeviceViewModel : ViewModelBase
{
    private readonly ILocalizationService _loc;
    private readonly ILogService _log;
    private readonly ReadOnlyObservableCollection<SdrPayloadRecordViewModel> _items;

    public SdrDeviceViewModel(ushort id) : base(SdrWellKnownUri.SdrShellPageSdrStoreDeviceIndex.FormatWith(id))
    {
        Name = $"Payload {id}";
        if (Design.IsDesignMode)
        {
            _items = new ReadOnlyObservableCollection<SdrPayloadRecordViewModel>(
                new ObservableCollection<SdrPayloadRecordViewModel>(new List<SdrPayloadRecordViewModel>
                {
                    new(Guid.NewGuid())
                    {
                        Name = "Record1",
                        CreatedDateTime = DateTime.Now.AddMinutes(-60),
                    },
                    new(Guid.NewGuid())
                    {
                        Name = "Record2",
                        CreatedDateTime = DateTime.Now.AddMinutes(-50),
                    },
                    new(Guid.NewGuid())
                    {
                        Name = "Record3",
                        CreatedDateTime = DateTime.Now.AddMinutes(-40),
                    }
                }));
        }
    }

    public SdrDeviceViewModel(ISdrClientDevice device, ILocalizationService loc, ILogService log) : this(
        device.Heartbeat.FullId)
    {
        _loc = loc;
        _log = log;
        Client = device;

        device.Sdr.Records
            .Transform(_ => new SdrPayloadRecordViewModel(device.Heartbeat.FullId, _, _log, _loc, device.Sdr))
            .SortBy(_ => IsSortByName ? _.Name : _.CreatedDateTime,
                IsSortByName ? SortDirection.Ascending : SortDirection.Descending)
            .Bind(out _items)
            .DisposeMany()
            .Subscribe()
            .DisposeItWith(Disposable);
        this.WhenValueChanged(_ => SelectedRecord)
            .Where(_ => _ != null)
            .Subscribe(_ => _.DownloadTags.Execute().Subscribe())
            .DisposeItWith(Disposable);
        DownloadRecords = ReactiveCommand.CreateFromTask(cancel =>
                device.Sdr.DownloadRecordList(new Progress<double>(_ => DownloadRecordsProgress = _), cancel))
            .DisposeItWith(Disposable);

        DownloadRecords.ThrownExceptions.Subscribe(_ => _log.Error("Record", "Error to download records", _))
            .DisposeItWith(Disposable);

        this.WhenAnyValue(_ => _.SelectedRecord)
            .Subscribe(_ => { IsAnySelected = _ != null; })
            .DisposeItWith(Disposable);

        this.WhenAnyValue(_ => _.IsSortByName)
            .Subscribe(_ => { DownloadRecords.Execute().Subscribe(); })
            .DisposeItWith(Disposable);
    }

    [Reactive] public bool IsSortByName { get; set; } = true;

    [Reactive] public double DownloadRecordsProgress { get; set; }
    public ReactiveCommand<Unit, bool> DownloadRecords { get; }

    public string Name { get; set; }

    public ReadOnlyObservableCollection<SdrPayloadRecordViewModel> Items => _items;

    [Reactive] public SdrPayloadRecordViewModel SelectedRecord { get; set; }

    [Reactive] public bool IsAnySelected { get; set; }

    public ISdrClientDevice Client { get; }

    public void TrySelect(Guid recordId)
    {
        var item = Items.FirstOrDefault(x => x.Record.Id == recordId);
        if (item == null) return;
        SelectedRecord = item;
    }
}