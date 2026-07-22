using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeCareersPage
    : PageBase
{
    private static readonly By StartYourSearchBtnLocator =
        By.PartialLinkText("START YOUR SEARCH");

    public HomeCareersPage(IWebDriverWrapper driver)
        : base(driver) { }

    public CareersPage ClickStartYourSearchHereButton()
    {
        Driver.Click(StartYourSearchBtnLocator);
        return new CareersPage(Driver);
    }
}