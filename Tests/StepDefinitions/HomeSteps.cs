using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public class HomeSteps
{
    private readonly HomePage HomePage;

    public HomeSteps(HomePage homePage)
    {
        HomePage = homePage;
    }

    [Given("I am on the home page")]
    public void GivenIAmOnTheHomePage()
    {
        HomePage.Open();
    }

    [When("I click the Insights button")]
    public void WhenIClickTheInsightsButton()
    {
        HomePage.ClickInsightsButton();
    }

    [When("I scroll to the footer")]
    public void WhenIScrollToTheFooter()
    {
        HomePage.GoToFooter();
    }

    [When("I click the magnifier button")]
    public void WhenIClickTheMagnifierButton()
    {
        HomePage.ClickMagnifierButton();
    }

    [When("I click the Careers button")]
    public void WhenIClickTheCareersButton()
    {
        HomePage.ClickCareersButton();
    }
}