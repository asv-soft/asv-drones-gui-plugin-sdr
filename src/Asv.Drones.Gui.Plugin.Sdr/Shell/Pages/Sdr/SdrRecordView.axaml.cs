using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr;

[ExportView(typeof(SdrRecordViewModel))]

public partial class SdrRecordView : ReactiveUserControl<SdrRecordViewModel>
{
    public SdrRecordView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}