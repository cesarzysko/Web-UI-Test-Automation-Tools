using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeSearchResultsPage
    : PageBase
{
    private static readonly By ArticlesLocator = // language=CSS
        By.CssSelector("div.search-results__items article");

    internal HomeSearchResultsPage(IWebDriverWrapper driver)
        : base(driver) { }

    public bool DoAllResultsContainInput(string input)
    {
        return Driver.DoAllContainText(ArticlesLocator, input);
    }
}