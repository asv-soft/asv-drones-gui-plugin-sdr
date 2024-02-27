using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr.Rtt.CustomControls;

[ExportView(typeof(GpSdrRttViewModel))]

public partial class GpSdrRttView : ReactiveUserControl<GpSdrRttViewModel>
{
    public GpSdrRttView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}