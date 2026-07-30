using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class InsightsBlogPage
    : PageBase
{
    private static readonly By HeaderLocator = // language=XPath
        By.XPath("//h1");

    private static readonly By AltHeaderLocator = // language=XPath
        By.XPath("//main/descendant::*[contains(@class, 'scaling-of-text-wrapper')]");

    public InsightsBlogPage(IWebDriverWrapper driver)
        : base(driver) { }

    public string GetArticleName()
    {
        By[] headerLocators = [HeaderLocator, AltHeaderLocator];
        var name = headerLocators.Select(locator =>
            {
                try
                {
                    Log.Info("Trying to read the article name.");
                    var name = Driver.GetText(locator);
                    Log.InfoFormat("Found article name \"{0}\".", name);
                    return name;
                }
                catch (NoSuchElementException)
                {
                    Log.InfoFormat("Could not find an article name using the locator \"{0}\".", locator);
                    return string.Empty;
                }
            })
            .FirstOrDefault(s => !string.IsNullOrWhiteSpace(s), string.Empty)!;

        if (string.IsNullOrWhiteSpace(name))
        {
            Log.Warn("Could not find the article name using any of the available locators.");
        }

        return name;
    }
}