using System.Composition;
using Asv.Cfg;
using Asv.Common;
using Asv.Drones.Gui.Api;
using Asv.Mavlink;

namespace Asv.Drones.Gui.Plugin.Sdr;

[ExportShellPage(Uri)]
public class SdrParamsViewModel:ParamPageViewModel
{
    public const string Uri = $"{WellKnownUri.ShellPage}.params-payload";
    [ImportingConstructor]
    public SdrParamsViewModel(IMavlinkDevicesService svc,ILogService log, IConfiguration cfg )
        :base(Uri,svc, log, cfg)
    {
        
    }

    public override IParamsClientEx? GetParamsClient(IMavlinkDevicesService svc, ushort fullId, DeviceClass @class)
    {
        var dev = svc.GetPayloadsByFullId(fullId);
        if (dev == null) return null;
        dev.Name.Subscribe(n=>Title = n).DisposeItWith(Disposable);
        return dev.Params;
    }
}