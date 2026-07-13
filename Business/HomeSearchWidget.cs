using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeSearchWidget
    : PageBase
{
    private static readonly By SearchInputLocator =
        By.TagName("input");

    private static readonly By SearchBtnLocator =
        By.ClassName("custom-search-button");

    internal HomeSearchWidget(IWebDriverWrapper driver)
        : base(driver) { }

    public HomeSearchWidget EnterSearchInput(string input)
    {
        Driver.SendKeys(SearchInputLocator, input);
        return this;
    }

    public HomeSearchResultsPage ClickSearchButton()
    {
        Driver.Click(SearchBtnLocator);
        return new HomeSearchResultsPage(Driver);
    }
}