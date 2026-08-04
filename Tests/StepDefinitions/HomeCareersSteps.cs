using Business;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public class HomeCareersSteps
{
    private readonly HomeCareersPage HomeCareersPage;

    public HomeCareersSteps(HomeCareersPage homeCareersPage)
    {
        HomeCareersPage = homeCareersPage;
    }

    [When("I click the Start Your Search Here button")]
    public void WhenIClickTheStartYourSearchHereButton()
    {
        HomeCareersPage.ClickStartYourSearchHereButton();
    }
}