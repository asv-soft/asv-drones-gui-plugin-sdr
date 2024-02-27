using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr;

[ExportView(typeof(SdrStoreBrowserViewModel))]

public partial class SdrStoreBrowserView : ReactiveUserControl<SdrStoreBrowserViewModel>
{
    public SdrStoreBrowserView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    
}