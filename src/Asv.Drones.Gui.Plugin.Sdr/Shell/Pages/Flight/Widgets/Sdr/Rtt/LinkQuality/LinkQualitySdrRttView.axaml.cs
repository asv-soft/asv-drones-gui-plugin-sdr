using Asv.Drones.Gui.Api;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Asv.Drones.Gui.Plugin.Sdr.Rtt.LinkQuality;

[ExportView(typeof(LinkQualitySdrRttViewModel))]

public partial class LinkQualitySdrRttView : UserControl
{
    public LinkQualitySdrRttView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}