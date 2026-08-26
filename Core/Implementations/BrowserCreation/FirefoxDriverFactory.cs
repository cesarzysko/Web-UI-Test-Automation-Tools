using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Core;

public sealed class FirefoxDriverFactory
    : IWebDriverFactory
{
    public Browser Browser => Browser.Firefox;

    private readonly BrowserSettings Settings;
    private readonly string DownloadPath;

    public FirefoxDriverFactory(IConfig config, IDownloadPathGetter downloadPathGetter)
    {
        Settings = config.Data.BrowserSettings;
        DownloadPath = downloadPathGetter.GetDownloadPath();
    }

    public IWebDriver CreateDriver()
    {
        var options = CreateFirefoxOptions();
        var driver = new FirefoxDriver(options);
        if (Settings.Maximized)
        {
            driver.Manage().Window.Maximize();
        }

        driver.SetImplicitWait(TimeSpan.FromSeconds(Settings.ImplicitWaitTimeSeconds));
        return driver;
    }

    private FirefoxOptions CreateFirefoxOptions()
    {
        FirefoxOptions options = new FirefoxOptions();
        options.HandleDownloads(DownloadPath);
        return options;
    }
}