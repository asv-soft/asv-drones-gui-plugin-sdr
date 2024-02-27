using System.Composition;
using Asv.Cfg;
using Asv.Drones.Gui.Api;
using Asv.Drones.Gui.Plugin.Sdr.Rtt;
using DynamicData;

namespace Asv.Drones.Gui.Plugin.Sdr;



[Export(WellKnownUri.ShellPageMapFlight, typeof(IViewModelProvider<IMapWidget>))]

public class FlightMissionWidgetProvider:ViewModelProviderBase<IMapWidget>
{
    [ImportingConstructor]
    public FlightMissionWidgetProvider(
        IMavlinkDevicesService devices,ILogService log,
        ILocalizationService localization,
        IConfiguration configuration,
        [ImportMany]IEnumerable<ISdrRttWidgetProvider> rtt)
    {
        devices.Payloads
            .Transform(_ => (IMapWidget)new FlightSdrViewModel(_,log, localization,configuration,rtt))
            .ChangeKey( ((_, v) => v.Id) )
            .PopulateInto(Source);
    }
}