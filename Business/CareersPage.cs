using Core;
using Core.Abstractions;
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

    public CareersPage(IWebDriverWrapper driver)
        : base(driver) { }

    public CareersPage SearchForRemotePosition(string keyword, string country)
    {
        Log.InfoFormat("Searching for a remote position with keyword \"{0}\" and county \"{1}\".", keyword, country);
        EnterKeyword(keyword);
        EnterCountry(country);
        CheckRemoteOption();
        ClickSearchButton();
        return this;
    }

    public string GetLatestResult()
    {
        ExpandLatestResult();
        Log.Info("Reading the text from the latest search result.");
        var text = Driver.GetText(LatestResultDescriptionLocator);
        Log.InfoFormat("Text from the latest search result: \"{0}\".", text);
        return text;
    }

    private void ExpandLatestResult()
    {
        Log.Info("Clicking the expand button on the latest search result.");
        Driver.Click(LatestResultExpanderLocator);
    }

    private void EnterKeyword(string keyword)
    {
        Log.InfoFormat("Entering \"{0}\" into the \"Keyword\" text input.", keyword);
        Driver.SendKeysWithEnter(RoleOrKeywordInputLocator, keyword);
    }

    private void EnterCountry(string country)
    {
        Log.InfoFormat("Entering \"{0}\" into the \"Country\" text input.", country);
        Driver.SendKeysWithEnter(CountryInputLocator, country);
    }

    private void CheckRemoteOption()
    {
        Log.Info("Checking the \"Remote\" checkbox.");
        Driver.ClickWithInterceptFallback(RemoteCheckboxLocator, AcceptCookies);
    }

    private void ClickSearchButton()
    {
        Log.Info("Clicking the \"Search\" button.");
        Driver.Click(PositionSearchBtnLocator);
    }

    private void AcceptCookies()
    {
        Log.Info("Clicking the \"Accept Cookies\" button.");
        Driver.Click(CookiesBtnLocator);
    }
}