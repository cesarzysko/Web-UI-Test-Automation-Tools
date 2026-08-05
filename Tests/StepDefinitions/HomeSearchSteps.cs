using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public sealed class HomeSearchSteps
{
    private readonly HomeSearchWidget HomeSearchWidget;

    public HomeSearchSteps(HomeSearchWidget homeSearchWidget)
    {
        HomeSearchWidget = homeSearchWidget;
    }

    [When("I enter (.*) into the search input")]
    public void WhenIEnterIntoTheSearchInput(string searchTerm)
    {
        HomeSearchWidget.EnterSearchInput(searchTerm);
    }

    [When("I click the search button")]
    public void WhenIClickTheSearchButton()
    {
        HomeSearchWidget.ClickSearchButton();
    }
}