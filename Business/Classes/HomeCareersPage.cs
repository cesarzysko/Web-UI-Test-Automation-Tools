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
        Log.Info("Clicking the \"START YOUR SEARCH HERE\" button.");
        Driver.Click(StartYourSearchBtnLocator);
        return new CareersPage(Driver);
    }
}