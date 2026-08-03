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

    private readonly IGestureController GestureController;
    private readonly IPageFactory PageFactory;

    public HomePage(string url, INavigator navigator, IElementInteractor interactor, IGestureController gestureController, IPageFactory pageFactory)
        : base(interactor)
    {
        navigator.NavigateToUrl(url);
        GestureController = gestureController;
        PageFactory = pageFactory;
    }

    public HomeCareersPage ClickCareersButton()
    {
        Log.Info("Clicking the \"Careers\" button.");
        Interactor.Click(CareersBtnLocator);
        return PageFactory.Create<HomeCareersPage>();
    }

    public HomeSearchWidget ClickMagnifierButton()
    {
        Log.Info("Clicking the \"Magnifier\" icon button.");
        Interactor.Click(MagnifierBtnLocator);
        return PageFactory.Create<HomeSearchWidget>();
    }

    public HomeFooterWidget GoToFooter()
    {
        Log.Info("Scrolling down to the footer.");
        GestureController.ScrollToElement(FooterLocator);
        return PageFactory.Create<HomeFooterWidget>();
    }

    public HomeInsightsPage ClickInsightsButton()
    {
        Log.Info("Clicking the \"Insights\" button.");
        Interactor.Click(InsightsBtnLocator);
        return PageFactory.Create<HomeInsightsPage>();
    }
}