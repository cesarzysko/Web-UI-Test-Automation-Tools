namespace Core;

public sealed class ConfigData
{
    public string MainPageUrl { get; init; } = string.Empty;
    public string ScreenshotsSubDirectory { get; init; } = string.Empty;
    public Browser Browser { get; init; } = Browser.Chrome;
    public BrowserSettings BrowserSettings { get; init; } = new();
    public ApiSettings ApiSettings { get; init; } = new();
    public LoggingSettings LoggingSettings { get; init; } = new();
}