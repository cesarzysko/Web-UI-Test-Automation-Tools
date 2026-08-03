using log4net;
using OpenQA.Selenium;

namespace Core;

public class WebElementInteractor
    : IElementInteractor
{
    private static readonly TimeSpan PageLoadTimeout = TimeSpan.FromSeconds(5);

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

    public void SendKeysWithEnter(By locator, string input)
    {
        SendKeys(locator, input + Keys.Enter);
    }

    public void SendKeys(By locator, string input)
    {
        Log.DebugFormat("Sending \"{0}\" keys to web element with locator \"{1}\".", input, locator);
        Finder.Find(locator).SendKeys(input);
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