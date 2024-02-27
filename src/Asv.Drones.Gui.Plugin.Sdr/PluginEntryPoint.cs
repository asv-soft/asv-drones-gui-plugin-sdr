using System.Composition;
using Asv.Cfg;
using Asv.Drones.Gui.Api;
using NLog;

namespace Asv.Drones.Gui.Plugin.Sdr;

[PluginEntryPoint("SDR")]
[Shared]
public class PluginEntryPoint:IPluginEntryPoint
{
    private Logger _log = LogManager.GetCurrentClassLogger();
    
    [ImportingConstructor]
    public PluginEntryPoint(IConfiguration cfg, IApplicationHost host)
    {
        
    }
    public async void Initialize()
    {
       

    }

    public void Init()
    {
        
    }

    public void OnFrameworkInitializationCompleted()
    {
        
    }

    public void OnShutdownRequested()
    {
        
    }
}