using System.Composition;
using Asv.Drones.Gui.Api;
using Material.Icons;

namespace Asv.Drones.Gui.Plugin.Sdr;

[Export(typeof(IShellMenuItem))]

public class SdrStoreShellMenuItem:ShellMenuItem
{
    public SdrStoreShellMenuItem():base($"{WellKnownUri.ShellPage}.sdr-store")
    {
        // DONE: Localize
        Name = RS.SdrStoreShellMenuItem_Name;
        NavigateTo = SdrWellKnownUri.SdrShellPageSdrStoreUri;
        Icon = MaterialIconDataProvider.GetData(MaterialIconKind.DatabaseOutline);
        Position = ShellMenuPosition.Top;
        Type = ShellMenuItemType.PageNavigation;
        Order = 0;
    }
}
