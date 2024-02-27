﻿using System.Composition;
using Asv.Cfg;
using Asv.Common;
using Asv.Drones.Gui.Api;
using DynamicData.Binding;
using ReactiveUI.Fody.Helpers;

namespace Asv.Drones.Gui.Plugin.Sdr;

public class SdrSettingsViewModel : ViewModelBase, ITreePage
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

}