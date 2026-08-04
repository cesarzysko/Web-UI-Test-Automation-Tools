using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public class HomeInsightsSteps
{
    private readonly HomeInsightsPage HomeInsightsPage;
    private readonly ScenarioContext Ctx;

    public HomeInsightsSteps(HomeInsightsPage homeInsightsPage, ScenarioContext ctx)
    {
        HomeInsightsPage = homeInsightsPage;
        Ctx = ctx;
    }

    [When("I swipe the carousel (.*) times")]
    public void WhenISwipeTheCarouselTimes(int swipeTimes)
    {
        HomeInsightsPage.SwipeCarousel(swipeTimes);
    }

    [When("I note the name of the currently displayed article")]
    public void WhenINoteTheNameOfTheCurrentlyDisplayedArticle()
    {
        Ctx["articleName"] = HomeInsightsPage.GetCurrentArticleName();
    }

    [When("I click the Read More button for the current article")]
    public void WhenIClickTheReadMoreButtonForTheCurrentArticle()
    {
        HomeInsightsPage.ClickReadMoreButtonForCurrentArticle();
    }
}