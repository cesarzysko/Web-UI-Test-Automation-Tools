using log4net;
using OpenQA.Selenium;

namespace Core;

public sealed class WebElementFinder
    : IElementFinder
{
    private readonly IWebDriver Driver;

    public WebElementFinder(IWebDriver driver)
    {
        Driver = driver;
    }

    private static ILog Log => LogManager.GetLogger(typeof(WebElementFinder));

    IWebElement IElementFinder.Find(By locator)
    {
        try
        {
            Log.InfoFormat("Trying to find web element with locator \"{0}\".", locator);
            var elem = Driver.FindElement(locator);
            Log.InfoFormat("Web element with locator \"{0}\" successfully found.", locator);
            return elem;
        }
        catch (Exception)
        {
            Log.WarnFormat("Could not find web element with locator \"{0}\".", locator);
            throw;
        }

    }

    IReadOnlyList<IWebElement> IElementFinder.FindAll(By locator)
    {
        Log.InfoFormat("Trying to find all web elements with locator \"{0}\".", locator);
        var elems = Driver.FindElements(locator);
        Log.InfoFormat("Count of found web elements with locator \"{0}\": \"{1}\".", locator, elems.Count);
        return elems;
    }
}