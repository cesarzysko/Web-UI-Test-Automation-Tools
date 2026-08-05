using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class ServiceSteps
{
    private readonly ServicePage ServicePage;

    public ServiceSteps(ServicePage servicePage)
    {
        ServicePage = servicePage;
    }

    [Then("the page title should be (.*)")]
    public void ThenThePageTitleShouldBe(string pageTitle)
    {
        var actualTitle = ServicePage.GetTitle();
        Assert.That(actualTitle, Is.EqualTo(pageTitle));
    }

    [Then("the Our Related Expertise section should be displayed")]
    public void ThenTheOurRelatedExpertiseSectionShouldBeDisplayed()
    {
        var exists = ServicePage.IsRelatedExpertisePresent();
        Assert.That(exists, Is.True);
    }
}