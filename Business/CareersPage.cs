using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class CareersPage
    : PageBase
{
    private static readonly By CookiesBtnLocator =
        By.Id("onetrust-accept-btn-handler");

    private static readonly By RoleOrKeywordInputLocator =
        By.Name("search");

    private static readonly By CountryInputLocator = // language=XPath
        By.XPath("//input[@aria-label='Choose your country']");

    private static readonly By RemoteCheckboxLocator = // language=XPath
        By.XPath("//span[text()='Remote']/ancestor::label");

    private static readonly By PositionSearchBtnLocator =
        By.Name("submit_search_box_button");

    private static readonly By LatestResultExpanderLocator = // language=XPath
        By.XPath("//span[contains(@class, 'AccordionSection_headerIconContainer')]");

    private static readonly By LatestResultDescriptionLocator = // language=XPath
        By.XPath("//div[contains(@class, 'JobCard_accordionHeader')]");

    internal CareersPage(IWebDriverWrapper driver)
        : base(driver) { }

    public CareersPage SearchForRemotePosition(string keyword, string country)
    {
        EnterKeyword(keyword);
        EnterCountry(country);
        CheckRemoteOption();
        ClickSearchButton();
        return this;
    }

    public bool DoesLatestResultContainKeyword(string keyword)
    {
        ExpandLatestResult();
        return Driver.DoesContainText(LatestResultDescriptionLocator, keyword);
    }

    private void ExpandLatestResult()
    {
        Driver.Click(LatestResultExpanderLocator);
    }

    private void EnterKeyword(string keyword)
    {
        Driver.SendKeysWithEnter(RoleOrKeywordInputLocator, keyword);
    }

    private void EnterCountry(string country)
    {
        Driver.SendKeysWithEnter(CountryInputLocator, country);
    }

    private void CheckRemoteOption()
    {
        Driver.ClickWithInterceptFallback(RemoteCheckboxLocator, AcceptCookies);
    }

    private void ClickSearchButton()
    {
        Driver.Click(PositionSearchBtnLocator);
    }

    private void AcceptCookies()
    {
        Driver.Click(CookiesBtnLocator);
    }
}