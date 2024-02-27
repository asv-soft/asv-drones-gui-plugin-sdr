using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr;

[ExportView(typeof(SdrStorePageViewModel))]

public partial class SdrStorePageView : ReactiveUserControl<SdrStorePageViewModel>
{
    public SdrStorePageView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}