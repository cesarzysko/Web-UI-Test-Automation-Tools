namespace Core;

public sealed class ConfigData
{
    public string MainPageUrl { get; init; } = string.Empty;
    public string ScreenshotsSubdirectory { get; init; } = string.Empty;
    public Browser Browser { get; init; } = Browser.Chrome;
    public BrowserSettings BrowserSettings { get; init; } = new();
    public LoggingSettings LoggingSettings { get; init; } = new();
}