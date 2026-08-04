using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public class HomeFooterSteps
{
    private readonly HomeFooterWidget HomeFooterWidget;

    public HomeFooterSteps(HomeFooterWidget homeFooterWidget)
    {
        HomeFooterWidget = homeFooterWidget;
    }

    [When("I click the Code of Ethical Conduct button")]
    public void WhenIClickTheCodeOfEthicalConductButton()
    {
        HomeFooterWidget.ClickCodeOfEthicalConductButton();
    }
}