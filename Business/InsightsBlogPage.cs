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

    public bool DoesArticleNameMatch(string name)
    {
        By[] headers = [HeaderLocator, AltHeaderLocator];
        return headers.Any(h =>
        {
            try
            {
                return Driver.DoesContainText(h, name);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        });
    }
}