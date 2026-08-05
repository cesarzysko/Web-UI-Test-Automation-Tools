using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class AIServiceSelectorSteps
{
    private readonly AIServiceSelectorCtx AIServiceSelectorCtx;

    public AIServiceSelectorSteps(AIServiceSelectorCtx aiServiceSelectorCtx)
    {
        AIServiceSelectorCtx = aiServiceSelectorCtx;
    }

    [When("I select the (.*) category")]
    public void WhenISelectTheCategory(string serviceCategory)
    {
        AIServiceSelectorCtx.ClickMatchingService(serviceCategory);
    }
}