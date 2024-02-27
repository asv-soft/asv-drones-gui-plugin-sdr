using Asv.Drones.Gui.Api;
using Asv.Mavlink;

namespace Asv.Drones.Gui.Plugin.Sdr
{
    public class FlightSdrWidgetBase:MapWidgetBase
    {
        public static Uri GenerateUri(ISdrClientDevice gbs, string name) => new($"{WellKnownUri.ShellPageMapFlight}/{gbs.Heartbeat.FullId}/{name}");
        
        protected FlightSdrWidgetBase():base(new Uri($"fordesigntime://{Guid.NewGuid()}"))
        {
            Payload = null!;
        }

        protected FlightSdrWidgetBase(ISdrClientDevice payload,Uri uri) : base(uri)
        {
            Payload = payload;
        }
        
        protected ISdrClientDevice Payload { get; }
        
        protected override void InternalAfterMapInit(IMap context)
        {
            
        }
    }
   
}