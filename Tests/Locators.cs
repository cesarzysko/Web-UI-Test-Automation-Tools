using OpenQA.Selenium;

namespace Tests;

public static class Locators
{
    public static readonly By CookiesBtnLocator =
        By.Id("onetrust-accept-btn-handler");

    // language=CSS
    public static readonly By CareersBtnLocator =
        By.CssSelector("ul li:last-child a[href='/careers']");

    public static readonly By StartYourSearchBtnLocator =
        By.PartialLinkText("START YOUR SEARCH");

    public static readonly By RoleOrKeywordInputLocator =
        By.Name("search");

    // language=XPath
    public static readonly By CountryInputLocator =
        By.XPath("//input[@aria-label='Choose your country']");

    // language=XPath
    public static readonly By RemoteCheckboxLocator =
        By.XPath("//span[text()='Remote']/ancestor::label");

    public static readonly By PositionSearchBtnLocator =
        By.Name("submit_search_box_button");

    // language=XPath
    public static readonly By LatestResultExpanderLocator =
        By.XPath("//span[contains(@class, 'AccordionSection_headerIconContainer')]");

    // language=XPath
    public static readonly By LatestResultDescriptionLocator =
        By.XPath("//div[contains(@class, 'JobCard_accordionHeader')]");

    public static readonly By MagnifierBtnLocator =
        By.ClassName("header-search__button");

    public static readonly By SearchInputLocator =
        By.TagName("input");

    public static readonly By SearchBtnLocator =
        By.ClassName("custom-search-button");

    // language=CSS
    public static readonly By ArticlesLocator =
        By.CssSelector("div.search-results__items article");
}