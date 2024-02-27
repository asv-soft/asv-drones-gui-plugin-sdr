using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr.Rtt.CustomControls;


[ExportView(typeof(VorSdrRttViewModel))]

public partial class VorSdrRttView : ReactiveUserControl<VorSdrRttViewModel>
{
    public VorSdrRttView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}