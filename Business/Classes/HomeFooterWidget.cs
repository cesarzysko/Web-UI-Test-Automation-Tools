using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeFooterWidget
    : PageBase
{
    private static readonly By CodeOfEthicalConductBtnLocator = // language=XPath
        By.XPath("//a[contains(@href, 'code-of-conduct')]");

    private readonly IElementInteractor Interactor;

    public HomeFooterWidget(IElementInteractor interactor)
    {
        Interactor = interactor;
    }

    public void ClickCodeOfEthicalConductButton()
    {
        Log.Info("Clicking the \"Code of Ethical Conduct\" button.");
        Interactor.ClickJS(CodeOfEthicalConductBtnLocator);
    }
}