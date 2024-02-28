using System.Composition;
using Asv.Drones.Gui.Api;
using Asv.Mavlink;
using Material.Icons;

namespace Asv.Drones.Gui.Plugin.Sdr;

[Export(nameof(DeviceClass.SdrPayload) ,typeof(IShellMenuItem<IClientDevice>))]
public class SdrParamsMenu:ShellMenuItem, IShellMenuItem<IClientDevice>
{
    
    
    public SdrParamsMenu() : base(SdrParamsViewModel.Uri)
    {
        Icon = MaterialIconDataProvider.GetData(MaterialIconKind.WrenchCog);
        Position = ShellMenuPosition.Top;
        Type = ShellMenuItemType.PageNavigation;
        Order = 100;
        Name = RS.SdrParamsMenu_SdrParamsMenu_Settings;
    }
    
    public IShellMenuItem Init(IClientDevice target)
    {
        NavigateTo = ParamPageViewModel.GenerateUri(SdrParamsViewModel.Uri,target.FullId, target.Class);
        return this;
    }


}