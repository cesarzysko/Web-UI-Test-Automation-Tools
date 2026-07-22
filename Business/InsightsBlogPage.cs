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
        return headerLocators.Select(locator =>
            {
                try
                {
                    return Driver.GetText(locator);
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            })
            .FirstOrDefault(string.IsNullOrWhiteSpace, string.Empty)!;
    }
}