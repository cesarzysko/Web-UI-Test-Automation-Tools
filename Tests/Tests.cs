using Core;

namespace Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
public sealed class Tests
    : TestBase
{
    private static readonly TimeSpan DownloadTimeout = TimeSpan.FromSeconds(5);

    [TestCase("C++", Country.Poland)]
    [TestCase("JavaScript", Country.Mexico)]
    [TestCase("Python", Country.Portugal)]
    public void PositionSearch_WithCriteria_ProgrammingLanguagePresentInDescription(
        string programmingLanguage,
        Country country)
    {
        // Act
        var latestResult = HomePage
            .ClickCareersButton()
            .ClickStartYourSearchHereButton()
            .SearchForRemotePosition(programmingLanguage, country.ToString())
            .GetLatestResult();
        // Assert
        Assert.That(latestResult, Does.Contain(programmingLanguage).IgnoreCase);
    }

    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void GlobalSearch_WithWord_AllLinkTextsContainWord(
        string input)
    {
        // Act
        var results = HomePage
            .ClickMagnifierButton()
            .EnterSearchInput(input)
            .ClickSearchButton()
            .GetAllResults();
        // Assert
        var nonMatching = results.WhereNotContains(input);
        Assert.That(nonMatching, Is.Empty,
            $"Expected all {results.Count} search results for \"{input}\" to contain that word, " +
            $"but {nonMatching.Count} did not:\n" +
            $"{string.Join('\n', nonMatching)}\n");
    }

    [TestCase("Code-Of-Conduct_01_26.pdf")]
    public void Download_CodeOfConduct_DownloadSuccessful(
        string fileName)
    {
        // Act
        HomePage.GoToFooter()
            .ClickCodeOfEthicalConductButton();
        var isDownloaded = Driver.IsFileDownloaded(fileName, DownloadTimeout);
        // Assert
        Assert.That(isDownloaded, Is.True);
    }

    [Test]
    public void ArticleCarousel_ReadMore_OpenedArticleNameMatchesCarouselArticle(
        [Range(0, 4)] int carouselSwipes)
    {
        // Act
        var articleName = HomePage
            .ClickInsightsButton()
            .SwipeCarousel(carouselSwipes)
            .GetCurrentArticleName(out string name)
            .ClickReadMoreButtonForCurrentArticle()
            .GetArticleName();
        // Assert
        Assert.That(articleName, Does.Contain(name).IgnoreCase);
    }
}