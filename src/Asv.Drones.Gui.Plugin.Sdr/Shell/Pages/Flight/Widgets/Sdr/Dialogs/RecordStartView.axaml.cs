using Asv.Drones.Gui.Api;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Asv.Drones.Gui.Plugin.Sdr.Dialogs;

[ExportView(typeof(RecordStartViewModel))]

public partial class RecordStartView : ReactiveUserControl<RecordStartViewModel>
{
    public RecordStartView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}