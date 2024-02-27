using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr.Rtt.CustomControls;

[ExportView(typeof(LlzSdrRttViewModel))]

public partial class LlzSdrRttView : ReactiveUserControl<LlzSdrRttViewModel>
{
    public LlzSdrRttView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}