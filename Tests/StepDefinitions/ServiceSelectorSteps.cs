using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class ServiceSelectorSteps
{
    private readonly ServiceSelectorCtx ServiceSelectorCtx;

    public ServiceSelectorSteps(ServiceSelectorCtx serviceSelectorCtx)
    {
        ServiceSelectorCtx = serviceSelectorCtx;
    }

    [When("I select the (.*) category")]
    public void WhenISelectTheCategory(string serviceCategory)
    {
        ServiceSelectorCtx.ClickMatchingService(serviceCategory);
    }
}