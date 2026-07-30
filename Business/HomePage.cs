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

    public HomePage(IWebDriverWrapper driver, IConfig config)
        : base(driver)
    {
        Driver.NavigateToUrl(config.Data.MainPageUrl);
    }

    public HomeCareersPage ClickCareersButton()
    {
        Log.Info("Clicking the \"Careers\" button.");
        Driver.Click(CareersBtnLocator);
        return new HomeCareersPage(Driver);
    }

    public HomeSearchWidget ClickMagnifierButton()
    {
        Log.Info("Clicking the \"Magnifier\" icon button.");
        Driver.Click(MagnifierBtnLocator);
        return new HomeSearchWidget(Driver);
    }

    public HomeFooterWidget GoToFooter()
    {
        Log.Info("Scrolling down to the footer.");
        Driver.ScrollToElement(FooterLocator);
        return new HomeFooterWidget(Driver);
    }

    public HomeInsightsPage ClickInsightsButton()
    {
        Log.Info("Clicking the \"Insights\" button.");
        Driver.Click(InsightsBtnLocator);
        return new HomeInsightsPage(Driver);
    }
}