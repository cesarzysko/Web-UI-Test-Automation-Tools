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

    private readonly IPageFactory PageFactory;

    public HomeSearchWidget(IElementInteractor interactor, IPageFactory pageFactory)
        : base(interactor)
    {
        PageFactory = pageFactory;
    }

    public HomeSearchWidget EnterSearchInput(string input)
    {
        Log.InfoFormat("Entering \"{0}\" into the \"Search\" text input.", input);
        Interactor.SendKeys(SearchInputLocator, input);
        return this;
    }

    public HomeSearchResultsPage ClickSearchButton()
    {
        Log.Info("Clicking the \"Search\" button.");
        Interactor.Click(SearchBtnLocator);
        return PageFactory.Create<HomeSearchResultsPage>();
    }
}