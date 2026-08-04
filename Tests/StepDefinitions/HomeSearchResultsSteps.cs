using Business;
using Core;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public class HomeSearchResultsSteps
{
    private readonly HomeSearchResultsPage HomeSearchResultsPage;

    public HomeSearchResultsSteps(HomeSearchResultsPage homeSearchResultsPage)
    {
        HomeSearchResultsPage = homeSearchResultsPage;
    }

    [Then("every result link text should contain (.*)")]
    public void ThenEveryResultLinkTextShouldContain(string searchTerm)
    {
        var results = HomeSearchResultsPage.GetAllResults();
        var nonMatching = results.WhereNotContains(searchTerm);
        Assert.That(nonMatching, Is.Empty,
            $"Expected all {results.Count} search results for \"{searchTerm}\" to contain that word, " +
            $"but {nonMatching.Count} did not:\n" +
            $"{string.Join('\n', nonMatching)}\n");
    }
}