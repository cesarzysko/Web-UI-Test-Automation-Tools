using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeCareersPage
    : PageBase
{
    private static readonly By StartYourSearchBtnLocator =
        By.PartialLinkText("START YOUR SEARCH");

    public HomeCareersPage(IElementInteractor interactor)
        : base(interactor) { }

    public void ClickStartYourSearchHereButton()
    {
        Log.Info("Clicking the \"START YOUR SEARCH HERE\" button.");
        Interactor.Click(StartYourSearchBtnLocator);
    }
}