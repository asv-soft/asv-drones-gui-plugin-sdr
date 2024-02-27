using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr;

[ExportView(typeof(FlightSdrViewModel))]

public partial class FlightSdrView : ReactiveUserControl<FlightSdrViewModel>
{
    public FlightSdrView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}