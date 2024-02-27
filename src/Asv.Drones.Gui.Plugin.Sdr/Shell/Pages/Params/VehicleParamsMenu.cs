using System.Composition;
using Asv.Drones.Gui.Api;
using Asv.Mavlink;
using Material.Icons;

namespace Asv.Drones.Gui.Plugin.Sdr;

[Export(nameof(DeviceClass.SdrPayload) ,typeof(IShellMenuItem<IClientDevice>))]
public class VehicleParamsMenu:ShellMenuItem, IShellMenuItem<IClientDevice>
{
    public const string Uri = $"{WellKnownUri.ShellPage}.params-payload";
    
    public VehicleParamsMenu() : base(Uri)
    {
        Icon = MaterialIconDataProvider.GetData(MaterialIconKind.WrenchCog);
        Position = ShellMenuPosition.Top;
        Type = ShellMenuItemType.PageNavigation;
        Order = 100;
        Name = "Settings";
    }
    
    public IShellMenuItem Init(IClientDevice target)
    {
        NavigateTo = ParamPageViewModel.GenerateUri(Uri,target.FullId, target.Class);
        return this;
    }


}