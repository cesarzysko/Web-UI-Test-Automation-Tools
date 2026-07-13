namespace Core;

public class ConfigData
{
    public string MainPageUrl { get; init; } = string.Empty;
    public Browser Browser { get; init; } = Browser.Chrome;
    public BrowserSettings BrowserSettings { get; init; } = new();
}