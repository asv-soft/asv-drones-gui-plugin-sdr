using System.Composition;
using Asv.Cfg;
using Asv.Drones.Gui.Api;
using Material.Icons;

namespace Asv.Drones.Gui.Plugin.Sdr;

[Export(WellKnownUri.ShellPageSettings,typeof(ITreePageMenuItem))]
[method: ImportingConstructor]
public class PluginsMarketTreeMenuItem(IConfiguration cfg, ILocalizationService loc)
    : TreePageMenuItem($"{WellKnownUri.ShellPageSettings}.sdr")
{
    public override Uri ParentId => WellKnownUri.UndefinedUri;
    public override string? Name => RS.SdrSettingsViewModel_Header;
    public override string? Description => RS.SdrSettingsViewModel_Description;
    public override MaterialIconKind Icon => SdrIconHelper.DefaultIcon;
    public override int Order => 500;
    public override ITreePage? CreatePage(ITreePageContext context)
    {
        return new SdrSettingsViewModel(cfg,loc);
    }
}