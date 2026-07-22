using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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

    public WebDriverWrapper(IWebDriver driver, ILogger? logger, IDownloadPathGetter? downloadPath)
    {
        Driver = driver;
        Logger = logger ?? NullLogger.Instance;
        DownloadPath = downloadPath?.GetDownloadPath() ?? string.Empty;
    }

    public void NavigateToUrl(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }

    public string GetText(By locator)
    {
        return Find(locator).Text;
    }

    public IReadOnlyList<string> GetTexts(By locator)
    {
        return FindAll(locator).Select(elem => elem.Text).ToList();
    }

    public int GetElementWidth(By locator)
    {
        return Find(locator).Size.Width;
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

    public void SwipeElement(By locator, int by, int msDuration, int msPause)
    {
        var elem = Find(locator);
        var pointer = new PointerInputDevice(PointerKind.Mouse);
        var sequence = new ActionSequence(pointer, 0);

        sequence.AddAction(pointer.CreatePointerMove(elem, 0, 0, TimeSpan.Zero));
        sequence.AddAction(pointer.CreatePointerDown(MouseButton.Left));

        var deltaTimeSpan = TimeSpan.FromMilliseconds(10);
        var deltaToTotal = deltaTimeSpan.TotalMilliseconds / msDuration;
        var byDelta = (int)(deltaToTotal * by);

        for (int current = 0; Math.Abs(current) < Math.Abs(by); current += byDelta)
        {
            sequence.AddAction(pointer.CreatePointerMove(CoordinateOrigin.Pointer, byDelta, 0, deltaTimeSpan));
        }

        sequence.AddAction(pointer.CreatePointerUp(MouseButton.Left));
        sequence.AddAction(pointer.CreatePointerMove(elem, 0, 0, TimeSpan.FromMilliseconds(msPause)));
        ((IActionExecutor)Driver).PerformActions([sequence]);
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