using Asv.Drones.Gui.Api;

namespace Asv.Drones.Gui.Plugin.Sdr;

public static class SdrWellKnownUri
{
    public const string SdrShellPageSdrStore = $"{WellKnownUri.ShellPage}.sdr-store";
    
    public const string SdrShellPageSdrStoreDeviceIndex = $"{SdrShellPageSdrStore}.device?id={{0}}";
    
    public const string SdrShellPageSdr = $"{WellKnownUri.ShellPage}.sdr";

    public const string DesignTime = $"{WellKnownUri.UriScheme}:designTime";
    
    public const string SdrShellPageSdrRec = $"{SdrShellPageSdr}.rec";
    
    public const string Sdr = $"{WellKnownUri.UriScheme}.sdr";
    
    public const string SdrDevice = $"{Sdr}.device";

    public const string SdrDeviceBrowser = $"{SdrDevice}.browser";

    public const string SdrDeviceBrowserIndex = $"{SdrDeviceBrowser}?id{{0}}";

}