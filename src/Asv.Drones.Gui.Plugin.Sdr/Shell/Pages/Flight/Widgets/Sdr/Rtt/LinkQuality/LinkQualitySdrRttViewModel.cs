using System.Composition;
using System.Reactive.Linq;
using Asv.Common;
using Asv.Mavlink;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Asv.Drones.Gui.Plugin.Sdr.Rtt.LinkQuality;

public class LinkQualitySdrRttViewModel : SdrRttItem
{
    public LinkQualitySdrRttViewModel()
    {
        
    }
    
    [ImportingConstructor]
    public LinkQualitySdrRttViewModel(ISdrClientDevice device) 
        : base(device, SdrRttItem.GenerateUri(device,$"linkquality"))
    {
        device.Heartbeat.LinkQuality
            .DistinctUntilChanged()
            .Sample(TimeSpan.FromMilliseconds(500))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => LinkQuality = _)
            .DisposeItWith(Disposable);
        
        device.Heartbeat.LinkQuality
            .Subscribe(_ => LinkQualityString = _.ToString("P0"))
            .DisposeItWith(Disposable);
    }
    
    [Reactive]
    public double LinkQuality { get; set; }
    
    [Reactive]
    public string LinkQualityString { get; set; } = RS.SdrRttItem_ValueNotAvailable;
}