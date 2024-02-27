using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr;

[ExportView(typeof(SdrPayloadBrowserViewModel))]

public partial class SdrPayloadBrowserView : ReactiveUserControl<SdrPayloadBrowserViewModel>
{
    public SdrPayloadBrowserView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}