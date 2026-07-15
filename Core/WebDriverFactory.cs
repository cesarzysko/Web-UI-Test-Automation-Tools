using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core;

public static class WebDriverFactory
{
    public static IWebDriver CreateDriver(ConfigData data, string downloadPath)
    {
        return data.Browser switch
        {
            Browser.Chrome => CreateChromeDriver(data.BrowserSettings, downloadPath),
            _ => throw new InvalidOperationException($"Browser type \"{data.Browser}\" not recognized.")
        };
    }

    private static ChromeDriver CreateChromeDriver(BrowserSettings settings, string downloadPath)
    {
        return new ChromeDriver(CreateChromeOptions(settings, downloadPath));
    }

    private static ChromeOptions CreateChromeOptions(BrowserSettings settings, string downloadPath)
    {
        ChromeOptions options = new ChromeOptions();
        options.HandleMaximized(settings.Maximized);
        options.HandleDownloads(downloadPath);
        return options;
    }
}