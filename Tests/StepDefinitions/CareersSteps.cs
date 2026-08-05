using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class CareersSteps
{
    private readonly CareersPage CareersPage;

    public CareersSteps(CareersPage careersPage)
    {
        CareersPage = careersPage;
    }

    [When("I search for a remote position with programming language (.*) and country (.*)")]
    public void WhenISearchForARemotePositionWithProgrammingLanguageAndCountry(string programmingLanguage, string country)
    {
        CareersPage.SearchForRemotePosition(programmingLanguage, country);
    }

    [Then("the latest search result should contain (.*) in its description")]
    public void ThenTheLatestSearchResultShouldContainInItsDescription(string programmingLanguage)
    {
        var latestResult = CareersPage.GetLatestResult();
        Assert.That(latestResult, Does.Contain(programmingLanguage).IgnoreCase);
    }
}