using System.Collections.ObjectModel;
using System.Reactive;
using Asv.Common;
using Asv.Drones.Gui.Api;
using Asv.Mavlink;
using Asv.Mavlink.V2.AsvSdr;
using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Asv.Drones.Gui.Plugin.Sdr;

public class SdrRecordViewModel : ViewModelBase, IActivatableViewModel
{
    private readonly ReadOnlyObservableCollection<TagViewModel> _tagItems;
    private readonly ReadOnlyObservableCollection<TagViewModel> _items;

    #region URI

    public static Uri GenerateUri(ushort deviceFullId, IAsvSdrClientRecord rec) =>
        new(SdrWellKnownUri.SdrShellPageSdrRec + $"?id={deviceFullId}&rec={rec.Id}");

    #endregion

    public SdrRecordViewModel() : base(new Uri(SdrWellKnownUri.DesignTime))
    {
        if (Design.IsDesignMode)
        {
            Name = "Record1";
            Description = "365 rec. (45 kb)";
            _tagItems = new ReadOnlyObservableCollection<TagViewModel>(new ObservableCollection<TagViewModel>(
                new List<TagViewModel>
                {
                    new(new TagId(Guid.NewGuid(), Guid.NewGuid()), "Tag1: 65.4654"),
                    new(new TagId(Guid.NewGuid(), Guid.NewGuid()), "Tag1: 65.4654"),
                    new(new TagId(Guid.NewGuid(), Guid.NewGuid()), "Tag1: 65.4654"),
                }));
        }
    }

    public SdrRecordViewModel(ushort deviceFullId, IAsvSdrClientRecord record, ILogService log,
        ILocalizationService loc) : base(GenerateUri(deviceFullId, record))
    {
        record.Name.Subscribe(_ => Name = _).DisposeItWith(Disposable);
        record.Created.Subscribe(_ => CreatedDateTime = _).DisposeItWith(Disposable);
        record.DataType.Subscribe(_ => Description = _.ToString("G")).DisposeItWith(Disposable);

        record.Tags
            .Transform(TransformRecordTag)
            .SortBy(_ => _.Name)
            .Bind(out _tagItems)
            .DisposeMany()
            .Subscribe()
            .DisposeItWith(Disposable);
        record.ByteSize
            .Subscribe(_ =>
                Description =
                    $"{record.DataCount.Value} rec. ({loc.ByteSize.ConvertToStringWithUnits(record.ByteSize.Value)})")
            .DisposeItWith(Disposable);
        record.DataCount
            .Subscribe(_ =>
                Description =
                    $"{record.DataCount.Value} rec. ({loc.ByteSize.ConvertToStringWithUnits(record.ByteSize.Value)})")
            .DisposeItWith(Disposable);

        DownloadTags = ReactiveCommand.CreateFromTask(cancel =>
                record.DownloadTagList(new Progress<double>(_ => TagsProgress = _), cancel))
            .DisposeItWith(Disposable);
        DownloadTags.ThrownExceptions.Subscribe(_ =>
            {
                if (Name != null) log.Error(Name, "Error to download records", _);
            })
            .DisposeItWith(Disposable);
        this.WhenActivated(disp => { disp.Add(DownloadTags.Execute().Subscribe()); });
    }

    private TagViewModel TransformRecordTag(AsvSdrClientRecordTag tag)
    {
        if (tag.Type == AsvSdrRecordTagType.AsvSdrRecordTagTypeInt64)
        {
            return new LongTagViewModel(tag.Id, tag.Name, tag.GetInt64());
        }
        else if (tag.Type == AsvSdrRecordTagType.AsvSdrRecordTagTypeUint64)
        {
            return new ULongTagViewModel(tag.Id, tag.Name, tag.GetUint64());
        }
        else if (tag.Type == AsvSdrRecordTagType.AsvSdrRecordTagTypeReal64)
        {
            return new DoubleTagViewModel(tag.Id, tag.Name, tag.GetReal64());
        }
        else if (tag.Type == AsvSdrRecordTagType.AsvSdrRecordTagTypeString8)
        {
            return new StringTagViewModel(tag.Id, tag.Name, tag.GetString());
        }

        return new TagViewModel(tag.Id, tag.Name);
    }

    [Reactive] public string? Name { get; set; }
    [Reactive] public DateTime CreatedDateTime { get; set; }
    [Reactive] public string Description { get; set; }
    [Reactive] public double TagsProgress { get; set; }
    public ReactiveCommand<Unit, bool> DownloadTags { get; }
    public ReadOnlyObservableCollection<TagViewModel> TagItems => _tagItems;
    public ReadOnlyObservableCollection<TagViewModel> Items => _items;

    public ViewModelActivator Activator { get; } = new();
}