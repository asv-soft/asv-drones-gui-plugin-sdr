using Asv.Drones.Gui.Api;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr;

[ExportView(typeof(SdrSettingsViewModel))]

public partial class SdrSettingsView : ReactiveUserControl<SdrSettingsViewModel>
{
    public SdrSettingsView()
    {
        InitializeComponent();
    }
}