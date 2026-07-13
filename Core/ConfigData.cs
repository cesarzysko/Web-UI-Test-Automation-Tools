namespace Core;

public class ConfigData
{
    public string Url { get; init; } = string.Empty;
    public Browser Browser { get; init; } = Browser.Chrome;
    public BrowserSettings BrowserSettings { get; init; } = new();
}