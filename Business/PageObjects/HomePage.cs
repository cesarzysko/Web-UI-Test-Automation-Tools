using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomePage
    : PageBase
{
    private static readonly By CareersBtnLocator = // language=CSS
        By.CssSelector("ul li:last-child a[href='/careers']");

    private static readonly By MagnifierBtnLocator =
        By.ClassName("header-search__button");

    private static readonly By InsightsBtnLocator =
        By.LinkText("Insights");

    private static readonly By FooterLocator =
        By.ClassName("copyright");

    private static readonly By MainNavigatorLocator = // language=XPath
        By.XPath("//button[contains(@class, 'hamburger-menu__button')]");

    private readonly IGestureController GestureController;
    private readonly INavigator Navigator;
    private readonly string Url;

    public HomePage(string url, INavigator navigator, IElementInteractor interactor, IGestureController gestureController)
        : base(interactor)
    {
        GestureController = gestureController;
        Navigator = navigator;
        Url = url;
    }

    public void Open()
    {
        Navigator.NavigateToUrl(Url);
    }

    public void ClickCareersButton()
    {
        Log.Info("Clicking the \"Careers\" button.");
        Interactor.Click(CareersBtnLocator);
    }

    public void ClickMagnifierButton()
    {
        Log.Info("Clicking the \"Magnifier\" icon button.");
        Interactor.Click(MagnifierBtnLocator);
    }

    public void GoToFooter()
    {
        Log.Info("Scrolling down to the footer.");
        GestureController.ScrollToElement(FooterLocator);
    }

    public void ClickInsightsButton()
    {
        Log.Info("Clicking the \"Insights\" button.");
        Interactor.Click(InsightsBtnLocator);
    }

    public void ClickMainNavigationButton()
    {
        Log.Info("Clicking the \"Main Navigation\" button.");
        Interactor.Click(MainNavigatorLocator);
    }
}