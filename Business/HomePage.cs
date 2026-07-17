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

    public HomePage(IWebDriverWrapper driver, string url)
        : base(driver)
    {
        Driver.NavigateToUrl(url);
    }

    public HomeCareersPage ClickCareersButton()
    {
        Driver.Click(CareersBtnLocator);
        return new HomeCareersPage(Driver);
    }

    public HomeSearchWidget ClickMagnifierButton()
    {
        Driver.Click(MagnifierBtnLocator);
        return new HomeSearchWidget(Driver);
    }

    public HomeFooterWidget GoToFooter()
    {
        Driver.ScrollToElement(By.ClassName("copyright"));
        return new HomeFooterWidget(Driver);
    }

    public HomeInsightsPage ClickInsightsButton()
    {
        Driver.Click(InsightsBtnLocator);
        return new HomeInsightsPage(Driver);
    }
}