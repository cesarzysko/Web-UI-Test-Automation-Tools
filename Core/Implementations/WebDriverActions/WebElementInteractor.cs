using System.Diagnostics;
using System.Drawing;
using log4net;
using OpenQA.Selenium;

namespace Core;

public class WebElementInteractor
    : IElementInteractor
{
    private static readonly TimeSpan PageLoadTimeout = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan MovementPollInterval = TimeSpan.FromMilliseconds(100);
    private static readonly TimeSpan MovementTimeout = TimeSpan.FromSeconds(5);

    private readonly IWebDriver Driver;
    private readonly IElementFinder Finder;

    public WebElementInteractor(IWebDriver driver, IElementFinder finder)
    {
        Driver = driver;
        Finder = finder;
    }

    private static ILog Log => LogManager.GetLogger(typeof(WebElementInteractor));

    public string GetText(By locator)
    {
        return Finder.Find(locator).Text;
    }

    public IReadOnlyList<string> GetTexts(By locator)
    {
        return Finder.FindAll(locator).Select(elem => elem.Text).ToList();
    }

    public int GetElementWidth(By locator)
    {
        return Finder.Find(locator).Size.Width;
    }

    public void Click(By locator)
    {
        try
        {
            Log.DebugFormat("Trying to click the web element with locator \"{0}\".", locator);
            Finder.Find(locator).Click();
        }
        catch (StaleElementReferenceException)
        {
            Log.WarnFormat("Web element with the locator \"{0}\" was stale.", locator);
            WaitUntilPageLoaded();
            Log.DebugFormat("Retrying to click the web element with locator \"{0}\".", locator);
            Finder.Find(locator).Click();
        }
    }

    public void ClickJS(By locator)
    {
        Log.DebugFormat("Trying to click the web element with locator \"{0}\".", locator);
        var js = ((IJavaScriptExecutor)Driver);
        var elem = Finder.Find(locator);
        js.ExecuteScript("arguments[0].click();", elem);
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

    public void DelayedClick(By locator)
    {
        Log.DebugFormat("Waiting for the web element with locator \"{0}\" to stop moving.", locator);
        //WaitUntilElementStopsMoving(locator);
        Thread.Sleep(TimeSpan.FromSeconds(2));
        Log.DebugFormat("Trying to click the web element with locator \"{0}\" after it stopped moving.", locator);
        Finder.Find(locator).Click();
    }

    private void WaitUntilElementStopsMoving(By locator)
    {
        var stopwatch = Stopwatch.StartNew();
        Point? previousLocation = null;

        while (stopwatch.Elapsed < MovementTimeout)
        {
            Point currentLocation;
            try
            {
                currentLocation = Finder.Find(locator).Location;
            }
            catch (StaleElementReferenceException)
            {
                Log.WarnFormat("Web element with locator \"{0}\" was stale while checking for movement.", locator);
                previousLocation = null;
                Thread.Sleep(MovementPollInterval);
                continue;
            }

            if (previousLocation.HasValue && currentLocation == previousLocation.Value)
            {
                Log.DebugFormat("Web element with locator \"{0}\" stopped moving at {1}.", locator, currentLocation);
                return;
            }

            previousLocation = currentLocation;
            Thread.Sleep(MovementPollInterval);
        }

        Log.WarnFormat(
            "Web element with locator \"{0}\" did not stop moving within {1}. Proceeding with click anyway.",
            locator, MovementTimeout);
    }

    public void SendKeysWithEnter(By locator, string input)
    {
        SendKeys(locator, input + Keys.Enter);
    }

    public void SendKeys(By locator, string input)
    {
        Log.DebugFormat("Sending \"{0}\" keys to web element with locator \"{1}\".", input, locator);
        Finder.Find(locator).SendKeys(input);
    }

    public bool Exists(By locator)
    {
        try
        {
            Finder.Find(locator);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void WaitUntilPageLoaded()
    {
        TimeSpan implicitWait = Driver.GetImplicitWait();
        Driver.SetImplicitWait(TimeSpan.Zero);
        try
        {
            Log.Debug("Waiting for page load.");
            Driver.GetExplicitWait(PageLoadTimeout).Until(
                dr => ((IJavaScriptExecutor) dr).ExecuteScript("return document.readyState")!.Equals("complete"));
            Log.Debug("Page load ended.");
        }
        finally
        {
            Driver.SetImplicitWait(implicitWait);
        }
    }
}