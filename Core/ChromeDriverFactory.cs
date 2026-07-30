using Core.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core;

public sealed class ChromeDriverFactory
    : IWebDriverFactory
{
    public Browser Browser => Browser.Chrome;

    private readonly BrowserSettings settings;
    private readonly string downloadPath;

    public ChromeDriverFactory(IConfig config, IDownloadPathGetter downloadPathGetter)
    {
        settings = config.Data.BrowserSettings;
        downloadPath = downloadPathGetter.GetDownloadPath();
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
        options.HandleMaximized(settings.Maximized);
        options.HandleDownloads(downloadPath);
        return options;
    }

    private void SetImplicitWaitTime(IWebDriver driver)
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitTimeSeconds);
    }
}