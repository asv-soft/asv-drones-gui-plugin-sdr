using System.Composition;
using Asv.Mavlink;
using Asv.Mavlink.V2.AsvSdr;

namespace Asv.Drones.Gui.Plugin.Sdr.Rtt.Llz;

[Export(typeof(ISdrRttItem))]

public class SdrRttItemLlzPowViewModel : SdrRttItemLlzViewModel
{
    public SdrRttItemLlzPowViewModel(ISdrClientDevice device) : base(device, "total/pow")
    {
    }

    public override double GetValue(AsvSdrRecordDataLlzPayload payload)
    {
        return payload.TotalPower;
    }
    
    public override string Title => "Pow";
    public override string Units => "dBm";
    public override string FormatString => "F0";
}
