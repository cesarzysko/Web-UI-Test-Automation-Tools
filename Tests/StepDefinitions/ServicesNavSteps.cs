using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class ServicesNavSteps
{
    private readonly ServicesNavCtx ServicesNavCtx;

    public ServicesNavSteps(ServicesNavCtx servicesNavCtx)
    {
        ServicesNavCtx = servicesNavCtx;
    }

    [When("I click the Artificial Intelligence dropdown button")]
    public void WhenIClickTheArtificialIntelligenceDropdownButton()
    {
        ServicesNavCtx.ClickArtificialIntelligenceDropdown();
    }
}