using System.Composition;
using Asv.Cfg;
using Asv.Common;
using Asv.Drones.Gui.Api;
using DynamicData.Binding;
using Material.Icons;
using ReactiveUI.Fody.Helpers;

namespace Asv.Drones.Gui.Plugin.Sdr;

public class SdrSettingsViewModel : TreePageViewModel
{
    

    private FlightSdrViewModelConfig _config; 
    
    private readonly IConfiguration _cfg;
    private readonly ILocalizationService _loc;
    
    private readonly IMeasureUnitItem<double, FrequencyUnits> _freqUnitInHz;
    private readonly IMeasureUnitItem<double, FrequencyUnits> _freqUnitInMHz;

    public SdrSettingsViewModel() : base(WellKnownUri.UndefinedUri)
    {
        DesignTime.ThrowIfNotDesignMode();
    
        HzFrequencyUnits = "Hz";
        MHzFrequencyUnits = "MHz";
        
        WriteFrequency = 5;
        ThinningFrequency = 5;
        
        GpFrequencyInMhz = "108";
        LlzFrequencyInMhz = "108";
        VorFrequencyInMhz = "108";

        LlzChannel = "18X";
        VorChannel = "17X";
    }

    [ImportingConstructor]
    public SdrSettingsViewModel(IConfiguration cfg, ILocalizationService loc) 
        : base($"{WellKnownUri.ShellPageSettings}.sdr")
    {
        _cfg = cfg;
        _loc = loc;

        _config = _cfg.Get<FlightSdrViewModelConfig>();

        WriteFrequency = _config.WriteFrequency;
        ThinningFrequency = _config.ThinningFrequency;
        GpFrequencyInMhz = _config.GpFrequencyInMhz;
        LlzFrequencyInMhz = _config.LlzFrequencyInMhz;
        VorFrequencyInMhz = _config.VorFrequencyInMhz;
        LlzChannel = _config.LlzChannel;
        VorChannel = _config.VorChannel;
        
        _freqUnitInHz = _loc.Frequency.AvailableUnits.FirstOrDefault(_ => _.Id == FrequencyUnits.Hz);
        _freqUnitInMHz = _loc.Frequency.AvailableUnits.FirstOrDefault(_ => _.Id == FrequencyUnits.MHz);

        HzFrequencyUnits = _freqUnitInHz?.Unit;
        MHzFrequencyUnits = _freqUnitInMHz?.Unit;
        
        this.WhenAnyPropertyChanged(nameof(WriteFrequency),
                nameof(ThinningFrequency),
                nameof(GpFrequencyInMhz),
                nameof(LlzFrequencyInMhz),
                nameof(VorFrequencyInMhz),
                nameof(LlzChannel),
                nameof(VorChannel))
            .Subscribe(_ =>
            {
                _config.WriteFrequency = WriteFrequency;
                _config.ThinningFrequency = ThinningFrequency;
                _config.GpFrequencyInMhz = GpFrequencyInMhz;
                _config.LlzFrequencyInMhz = LlzFrequencyInMhz;
                _config.VorFrequencyInMhz = VorFrequencyInMhz;
                _config.LlzChannel = LlzChannel;
                _config.VorChannel = VorChannel;
                
                _cfg.Set(_config);
            })
            .DisposeItWith(Disposable);
        
        _loc.DdmLlz.CurrentUnit.Subscribe(v => SelectedDdmLlzUnit = v)
            .DisposeItWith(Disposable);
        this.WhenValueChanged(v => v.SelectedDdmLlzUnit,false)
            .Subscribe(_loc.DdmLlz.CurrentUnit!)
            .DisposeItWith(Disposable);
            
        _loc.DdmGp.CurrentUnit.Subscribe(v => SelectedDdmGpUnit = v)
            .DisposeItWith(Disposable);
        this.WhenValueChanged(v => v.SelectedDdmGpUnit,false)
            .Subscribe(_loc.DdmGp.CurrentUnit!)
            .DisposeItWith(Disposable);
        
        _loc.Bearing.CurrentUnit.Subscribe(v => SelectedBearingUnit = v)
                     .DisposeItWith(Disposable);
        this.WhenValueChanged(v => v.SelectedBearingUnit,false)
            .Subscribe(_loc.Bearing.CurrentUnit!)
            .DisposeItWith(Disposable);
    }

    [Reactive]
    public string HzFrequencyUnits { get; set; }

    [Reactive]
    public string MHzFrequencyUnits { get; set; }

    [Reactive]
    public float WriteFrequency { get; set; }
    
    [Reactive]
    public uint ThinningFrequency { get; set; }
    
    [Reactive]
    public string GpFrequencyInMhz { get; set; }

    [Reactive]
    public string LlzFrequencyInMhz { get; set; }
    
    [Reactive]
    public string VorFrequencyInMhz { get; set; }

    [Reactive]
    public string LlzChannel { get; set; }

    [Reactive]
    public string VorChannel { get; set; }

    [Reactive]
    public IMeasureUnitItem<double,DdmUnits> SelectedDdmLlzUnit { get; set; }
    public IEnumerable<IMeasureUnitItem<double,DdmUnits>> DdmLlzUnits => _loc.DdmLlz.AvailableUnits;
        
    [Reactive]
    public IMeasureUnitItem<double,DdmUnits> SelectedDdmGpUnit { get; set; }
    public IEnumerable<IMeasureUnitItem<double,DdmUnits>> DdmGpUnits => _loc.DdmGp.AvailableUnits;
    
    [Reactive]
    public IMeasureUnitItem<double, BearingUnits> SelectedBearingUnit { get; set; }
    public IEnumerable<IMeasureUnitItem<double, BearingUnits>> BearingUnits => 
        _loc.Bearing.AvailableUnits;
    
    public string AngleIcon => MaterialIconDataProvider.GetData(MaterialIconKind.AngleAcute);
}