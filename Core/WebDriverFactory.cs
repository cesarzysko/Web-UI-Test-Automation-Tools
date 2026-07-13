using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core;

public static class WebDriverFactory
{
    public static IWebDriver CreateDriver(ConfigData data)
    {
        return data.Browser switch
        {
            Browser.Chrome => CreateChromeDriver(data.BrowserSettings),
            _ => throw new InvalidOperationException($"Browser type \"{data.Browser}\" not recognized.")
        };
    }

    private static ChromeDriver CreateChromeDriver(BrowserSettings settings)
    {
        return new ChromeDriver(CreateChromeOptions(settings));
    }

    private static ChromeOptions CreateChromeOptions(BrowserSettings settings)
    {
        ChromeOptions options = new ChromeOptions();
        if (settings.Maximized)
        {
            options.AddArguments("start-maximized");
        }

        return options;
    }
}