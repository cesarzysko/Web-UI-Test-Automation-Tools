using Core;
using OpenQA.Selenium;

namespace Business;

public class HomeFooterWidget
    : PageBase
{
    private static readonly By CodeOfEthicalConductBtnLocator = // language=XPath
        By.XPath("//a[contains(@href, 'code-of-conduct')]");

    public HomeFooterWidget(IWebDriverWrapper driver)
        : base(driver) { }

    public void ClickCodeOfEthicalConductButton()
    {
        Driver.ClickJS(CodeOfEthicalConductBtnLocator);
    }
}