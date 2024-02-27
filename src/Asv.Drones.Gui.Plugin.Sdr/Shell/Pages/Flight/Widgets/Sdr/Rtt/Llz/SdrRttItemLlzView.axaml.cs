using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr.Rtt.Llz;

[ExportView(typeof(SdrRttItemLlzViewModel))]

public partial class SdrRttItemLlzView : ReactiveUserControl<SdrRttItemLlzViewModel>
{
    public SdrRttItemLlzView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}