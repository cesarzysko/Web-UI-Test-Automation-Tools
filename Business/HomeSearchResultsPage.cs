using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeSearchResultsPage
    : PageBase
{
    private static readonly By ArticlesLocator = // language=CSS
        By.CssSelector("div.search-results__items article");

    public HomeSearchResultsPage(IWebDriverWrapper driver)
        : base(driver) { }

    public IReadOnlyList<string> GetAllResults()
    {
        return Driver.GetTexts(ArticlesLocator);
    }
}