using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class ServicesNavCtx
    : PageBase
{
    private static readonly By AIDropdownLocator = // language=XPath
        By.XPath("//a[contains(@href, '/services/artificial-intelligence')]//following-sibling::div");

    public ServicesNavCtx(IElementInteractor interactor)
        : base(interactor) { }

    public void ClickArtificialIntelligenceDropdown()
    {
        Log.Info("Clicking the \"Artificial Intelligence\" dropdown button.");
        Interactor.Click(AIDropdownLocator);
    }
}