using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core;

public sealed class ChromeDriverFactory
    : IWebDriverFactory
{
    public Browser Browser => Browser.Chrome;

    private readonly BrowserSettings Settings;
    private readonly string DownloadPath;

    public ChromeDriverFactory(IConfig config, IDownloadPathGetter downloadPathGetter)
    {
        Settings = config.Data.BrowserSettings;
        DownloadPath = downloadPathGetter.GetDownloadPath();
    }

    public IWebDriver CreateDriver()
    {
        var options = CreateChromeOptions();
        var driver = new ChromeDriver(options);
        SetImplicitWaitTime(driver);
        return driver;
    }

    private ChromeOptions CreateChromeOptions()
    {
        ChromeOptions options = new ChromeOptions();
        options.HandleMaximized(Settings.Maximized);
        options.HandleDownloads(DownloadPath);
        return options;
    }

    private void SetImplicitWaitTime(IWebDriver driver)
    {
        var implicitWait = TimeSpan.FromSeconds(Settings.ImplicitWaitTimeSeconds);
        driver.SetImplicitWait(implicitWait);
    }
}