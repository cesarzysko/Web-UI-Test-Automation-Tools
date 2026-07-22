using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Core;

public sealed class WebDriverFactory
{
    private readonly ConfigData data;
    private readonly string downloadPath;

    public WebDriverFactory(IConfig config, IDownloadPathGetter downloadPathGetter)
    {
        data = config.Data;
        downloadPath = downloadPathGetter.GetDownloadPath();
    }

    public IWebDriver CreateDriver()
    {
        return data.Browser switch
        {
            Browser.Chrome => CreateChromeDriver(),
            _ => throw new InvalidOperationException($"Browser type \"{data.Browser}\" not recognized.")
        };
    }

    private ChromeDriver CreateChromeDriver()
    {
        var driver = new ChromeDriver(CreateChromeOptions());
        SetImplicitWaitTime(driver);
        return driver;
    }

    private ChromeOptions CreateChromeOptions()
    {
        ChromeOptions options = new ChromeOptions();
        options.HandleMaximized(data.BrowserSettings.Maximized);
        options.HandleDownloads(downloadPath);
        return options;
    }

    private void SetImplicitWaitTime(IWebDriver driver)
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(data.BrowserSettings.ImplicitWaitTimeSeconds);
    }
}