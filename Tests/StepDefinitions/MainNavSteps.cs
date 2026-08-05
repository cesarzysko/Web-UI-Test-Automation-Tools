using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class MainNavSteps
{
    private readonly MainNavWidget MainNavWidget;

    public MainNavSteps(MainNavWidget mainNavWidget)
    {
        MainNavWidget = mainNavWidget;
    }

    [When("I click the Services dropdown arrow")]
    public void WhenIClickTheServicesDropdownArrow()
    {
        MainNavWidget.ClickServicesDropdown();
    }
}