using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeCareersPage
    : PageBase
{
    private static readonly By StartYourSearchBtnLocator =
        By.PartialLinkText("START YOUR SEARCH");

    private readonly IPageFactory PageFactory;

    public HomeCareersPage(IElementInteractor interactor, IPageFactory pageFactory)
        : base(interactor)
    {
        PageFactory = pageFactory;
    }

    public CareersPage ClickStartYourSearchHereButton()
    {
        Log.Info("Clicking the \"START YOUR SEARCH HERE\" button.");
        Interactor.Click(StartYourSearchBtnLocator);
        return PageFactory.Create<CareersPage>();
    }
}