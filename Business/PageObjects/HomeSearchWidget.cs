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

    public HomeSearchWidget(IElementInteractor interactor)
        : base(interactor) { }

    public void EnterSearchInput(string input)
    {
        Log.InfoFormat("Entering \"{0}\" into the \"Search\" text input.", input);
        Interactor.SendKeys(SearchInputLocator, input);
    }

    public void ClickSearchButton()
    {
        Log.Info("Clicking the \"Search\" button.");
        Interactor.Click(SearchBtnLocator);
    }
}