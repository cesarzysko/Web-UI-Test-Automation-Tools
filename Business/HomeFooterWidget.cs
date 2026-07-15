using Core;
using OpenQA.Selenium;

namespace Business;

public class HomeFooterWidget
    : PageBase
{
    private static readonly By CodeOfEthicalConductBtnLocator = // language=XPath
        By.XPath("//a[contains(@href, 'code-of-conduct')]");

    internal HomeFooterWidget(IWebDriverWrapper driver)
        : base(driver) { }

    public HomeFooterWidget ClickCodeOfEthicalConductButton()
    {
        Driver.ClickJS(CodeOfEthicalConductBtnLocator);
        return this;
    }
}