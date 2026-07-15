using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Core;

public sealed partial class WebDriverWrapper
    : IWebDriverWrapper
{
    private const int PageLoadTimeoutSeconds = 5;

    private readonly ILogger Logger;
    private readonly IWebDriver Driver;
    private readonly string DownloadPath;

    public WebDriverWrapper(IWebDriver driver, ILogger? logger, string? downloadPath)
    {
        Driver = driver;
        Logger = logger ?? NullLogger.Instance;
        DownloadPath = downloadPath ?? DownloadUtils.GetNewDownloadPath();
    }

    public void NavigateToUrl(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }

    public void SetImplicitWaitInSeconds(float seconds)
    {
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
    }

    public bool DoAllContainText(By locator, string text)
    {
        return FindAll(locator).All(elem =>
        {
            string str = elem.Text;
            bool check = StringUtils.DoesContainText(str, text);
            if (check)
            {
                return true;
            }

            LogTextNotFound(text, str);
            return false;
        });
    }

    public bool DoesContainText(By locator, string text)
    {
        string str = Find(locator).Text;
        bool check = StringUtils.DoesContainText(str, text);
        if (!check)
        {
            LogTextNotFound(text, str);
        }

        return check;
    }

    public void Click(By locator)
    {
        try
        {
            Find(locator).Click();
        }
        catch (StaleElementReferenceException)
        {
            WaitUntilPageLoaded();
            Find(locator).Click();
        }
    }

    public void ClickWithInterceptFallback(By locator, Action onClickIntercepted)
    {
        try
        {
            Click(locator);
        }
        catch (ElementClickInterceptedException)
        {
            onClickIntercepted.Invoke();
            Click(locator);
        }
    }

    public void ClickJS(By locator)
    {
        var js = ((IJavaScriptExecutor)Driver);
        var elem = Find(locator);
        js.ExecuteScript("arguments[0].click();", elem);
    }

    public void ScrollToElement(By locator)
    {
        var js = (IJavaScriptExecutor)Driver;
        int previousHeight = -1;
        int currentTries = 0;
        const int TriesUntilStable = 5;
        while (currentTries < TriesUntilStable)
        {
            int currentHeight = Convert.ToInt32(js.ExecuteScript("return document.scrollingElement.scrollHeight"));
            if (currentHeight == previousHeight)
            {
                currentTries++;
            }
            else
            {
                previousHeight = currentHeight;
                currentTries = 0;
            }

            Thread.Sleep(200);
        }

        js.ExecuteScript("arguments[0].scrollIntoView({block:'end'});", Find(locator));
    }

    public void SendKeysWithEnter(By locator, string input)
    {
        SendKeys(locator, input + Keys.Enter);
    }

    public void SendKeys(By locator, string input)
    {
        Find(locator).SendKeys(input);
    }

    public bool IsFileDownloaded(string fileName, TimeSpan timeout)
    {
        string filePath = Path.Combine(DownloadPath, fileName);
        Stopwatch sw = Stopwatch.StartNew();
        while (sw.Elapsed < timeout)
        {
            if (File.Exists(filePath))
            {
                return true;
            }
        }

        return File.Exists(filePath);
    }

    private void SetImplicitWaitInTimeSpan(TimeSpan timeSpan)
    {
        Driver.Manage().Timeouts().ImplicitWait = timeSpan;
    }

    private IReadOnlyList<IWebElement> FindAll(By locator)
    {
        return Driver.FindElements(locator);
    }

    private IWebElement Find(By locator)
    {
        return Driver.FindElement(locator);
    }

    private WebDriverWait GetExplicitWaitFromSeconds(float seconds)
    {
        return new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
    }

    private void WaitUntilPageLoaded()
    {
        TimeSpan implicitWait = Driver.Manage().Timeouts().ImplicitWait;
        SetImplicitWaitInTimeSpan(TimeSpan.Zero);
        try
        {
            GetExplicitWaitFromSeconds(PageLoadTimeoutSeconds).Until(
                dr => ((IJavaScriptExecutor) dr).ExecuteScript("return document.readyState")!.Equals("complete"));
        }
        finally
        {
            SetImplicitWaitInTimeSpan(implicitWait);
        }
    }

    [LoggerMessage(Level = LogLevel.Warning, Message = "Could not find \"{Text}\" in \"{Str}\".")]
    private partial void LogTextNotFound(string text, string str);

    void IDisposable.Dispose()
    {
        Driver.Dispose();
    }
}