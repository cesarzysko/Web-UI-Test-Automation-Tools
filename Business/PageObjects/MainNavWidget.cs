using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class MainNavWidget
    : PageBase
{
    private static readonly By ServicesDropdownLocator = // language=XPath
        By.XPath("//span[contains(string(.), 'Services')]//following-sibling::div");

    public MainNavWidget(IElementInteractor interactor)
        : base(interactor) { }

    public void ClickServicesDropdown()
    {
        Log.Info("Clicking the \"Services\" dropdown button.");
        Interactor.Click(ServicesDropdownLocator);
    }
}