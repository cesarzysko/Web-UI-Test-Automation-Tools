using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class InsightsBlogSteps
{
    private readonly InsightsBlogPage InsightsBlogPage;
    private readonly ScenarioContext Ctx;

    public InsightsBlogSteps(InsightsBlogPage insightsBlogPage, ScenarioContext ctx)
    {
        InsightsBlogPage = insightsBlogPage;
        Ctx = ctx;
    }

    [Then("the opened article name should contain the noted article name")]
    public void ThenTheOpenedArticleNameShouldContainTheNotedArticleName()
    {
        string name = (string)Ctx["articleName"];
        string articleName = InsightsBlogPage.GetMatchingArticleName(name);
        Assert.That(articleName, Does.Contain(name).IgnoreCase);
    }
}