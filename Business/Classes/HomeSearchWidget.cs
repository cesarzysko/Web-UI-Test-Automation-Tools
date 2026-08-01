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

    public HomeSearchWidget(IWebDriverWrapper driver)
        : base(driver) { }

    public HomeSearchWidget EnterSearchInput(string input)
    {
        Log.InfoFormat("Entering \"{0}\" into the \"Search\" text input.", input);
        Driver.SendKeys(SearchInputLocator, input);
        return this;
    }

    public HomeSearchResultsPage ClickSearchButton()
    {
        Log.Info("Clicking the \"Search\" button.");
        Driver.Click(SearchBtnLocator);
        return new HomeSearchResultsPage(Driver);
    }
}