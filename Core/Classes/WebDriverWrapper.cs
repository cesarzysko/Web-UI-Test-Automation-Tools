using System.Diagnostics;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Core;

public sealed class WebDriverWrapper
    : IWebDriverWrapper
{
    private const int PageLoadTimeoutSeconds = 5;

    private readonly IWebDriver driver;
    private readonly string downloadPath;
    private readonly string screenshotPath;

    private static ILog Log => LogManager.GetLogger(typeof(WebDriverWrapper));

    public WebDriverWrapper(IWebDriver driver, IConfig config, IDownloadPathGetter downloadPath)
    {
        this.driver = driver;
        screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), config.Data.ScreenshotsSubdirectory);
        this.downloadPath = downloadPath.GetDownloadPath();
    }

    public void NavigateToUrl(string url)
    {
        driver.Navigate().GoToUrl(url);
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
            Log.InfoFormat("Trying to click the web element with locator \"{0}\".", locator);
            Find(locator).Click();
        }
        catch (StaleElementReferenceException)
        {
            Log.WarnFormat("Web element with the locator \"{0}\" was stale.", locator);
            WaitUntilPageLoaded();
            Log.InfoFormat("Retrying to click the web element with locator \"{0}\".", locator);
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
            Log.WarnFormat("Click on web element with locator \"{0}\" was intercepted. Invoking fallback.", locator);
            onClickIntercepted.Invoke();
            Click(locator);
        }
    }

    public void ClickJS(By locator)
    {
        Log.InfoFormat("Trying to click the web element with locator \"{0}\".", locator);
        var js = ((IJavaScriptExecutor)driver);
        var elem = Find(locator);
        js.ExecuteScript("arguments[0].click();", elem);
    }

    public void ScrollToElement(By locator)
    {
        const int TriesUntilStable = 5;
        const int TrySleepMs = 200;

        var js = (IJavaScriptExecutor)driver;
        int previousHeight = -1;
        int currentTries = 0;
        Stopwatch sw = Stopwatch.StartNew();
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

            Thread.Sleep(TrySleepMs);
        }

        sw.Stop();
        Log.InfoFormat("Scroll height stabilized after {0}ms.", sw.Elapsed.TotalMilliseconds);
        Log.InfoFormat("Scrolling down to web element with locator \"{0}\".", locator);
        js.ExecuteScript("arguments[0].scrollIntoView({block:'end'});", Find(locator));
    }

    public void SwipeElementHorizontally(By locator, int by, int msDuration, int msPause)
    {
        Log.InfoFormat("Swiping web element with locator \"{0}\" horizontally by {1}px over {2}ms.", locator, by, msDuration);
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
        ((IActionExecutor)driver).PerformActions([sequence]);
        Log.InfoFormat("Swiping web element with locator \"{0}\" completed.", locator);
    }

    public void SendKeysWithEnter(By locator, string input)
    {
        SendKeys(locator, input + Keys.Enter);
    }

    public void SendKeys(By locator, string input)
    {
        Log.InfoFormat("Sending \"{0}\" keys to web element with locator \"{1}\".", input, locator);
        Find(locator).SendKeys(input);
    }

    public bool IsFileDownloaded(string fileName, TimeSpan timeout)
    {
        TimeSpan SleepTime = TimeSpan.FromMilliseconds(50);
        string filePath = Path.Combine(downloadPath, fileName);
        Stopwatch sw = Stopwatch.StartNew();
        while (sw.Elapsed < timeout)
        {
            Thread.Sleep(SleepTime);
            if (!File.Exists(filePath))
            {
                continue;
            }

            Log.InfoFormat("File \"{0}\" was found after waiting for {1} seconds.", filePath, sw.Elapsed.TotalSeconds);
            return true;
        }

        Log.WarnFormat("File \"{0}\" was not found after waiting for {1} seconds.", filePath, timeout.TotalSeconds);
        return false;
    }

    public void TakeScreenshot(string name)
    {
        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        var filePath = GetScreenshotPath(name);
        Log.InfoFormat("Saving screenshot at \"{0}\".", filePath);
        screenshot.SaveAsFile(filePath);
    }

    private string GetScreenshotPath(string name)
    {
        var fileName = GetScreenshotFileName(name);
        return Path.Combine(screenshotPath, fileName);
    }

    private static string GetScreenshotFileName(string name)
    {
        var now = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");
        return $"{now}_{name}";
    }

    private void SetImplicitWaitInTimeSpan(TimeSpan timeSpan)
    {
        driver.SetImplicitWait(timeSpan);
    }

    private IReadOnlyList<IWebElement> FindAll(By locator)
    {
        Log.InfoFormat("Trying to find all web elements with locator \"{0}\".", locator);
        var elems = driver.FindElements(locator);
        Log.InfoFormat("Count of found web elements with locator \"{0}\": \"{1}\".", locator, elems.Count);
        return elems;
    }

    private IWebElement Find(By locator)
    {
        try
        {
            Log.InfoFormat("Trying to find web element with locator \"{0}\".", locator);
            var elem = driver.FindElement(locator);
            Log.InfoFormat("Web element with locator \"{0}\" successfully found.", locator);
            return elem;
        }
        catch (Exception)
        {
            Log.WarnFormat("Could not find web element with locator \"{0}\".", locator);
            throw;
        }

    }

    private WebDriverWait GetExplicitWaitFromSeconds(float seconds)
    {
        return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
    }

    private void WaitUntilPageLoaded()
    {
        TimeSpan implicitWait = driver.Manage().Timeouts().ImplicitWait;
        SetImplicitWaitInTimeSpan(TimeSpan.Zero);
        try
        {
            Log.Info("Waiting for page load.");
            GetExplicitWaitFromSeconds(PageLoadTimeoutSeconds).Until(
                dr => ((IJavaScriptExecutor) dr).ExecuteScript("return document.readyState")!.Equals("complete"));
            Log.Info("Page load ended.");
        }
        finally
        {
            SetImplicitWaitInTimeSpan(implicitWait);
        }
    }

    void IDisposable.Dispose()
    {
        driver.Dispose();
    }
}