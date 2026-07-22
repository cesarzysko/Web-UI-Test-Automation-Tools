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
        var result = HomePage
            .ClickCareersButton()
            .ClickStartYourSearchHereButton()
            .SearchForRemotePosition(programmingLanguage, country.ToString())
            .DoesLatestResultContainKeyword(programmingLanguage);
        // Assert
        Assert.That(result, Is.True);
    }

    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void GlobalSearch_WithWord_AllLinkTextsContainWord(
        string input)
    {
        // Act
        var result = HomePage
            .ClickMagnifierButton()
            .EnterSearchInput(input)
            .ClickSearchButton()
            .DoAllResultsContainInput(input);
        // Assert
        Assert.That(result, Is.True);
    }

    [TestCase("Code-Of-Conduct_01_26.pdf")]
    public void Download_CodeOfConduct_DownloadSuccessful(
        string fileName)
    {
        // Act
        HomePage
            .GoToFooter()
            .ClickCodeOfEthicalConductButton();
        var result = Driver.IsFileDownloaded(fileName, DownloadTimeout);
        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ArticleCarousel_ReadMore_ArticleNameMatchesCarousel(
        [Range(0, 4)] int carouselSwipes)
    {
        // Act
        var result = HomePage
            .ClickInsightsButton()
            .SwipeCarousel(carouselSwipes)
            .GetCurrentArticleName(out string name)
            .ClickReadMoreButtonForCurrentArticle()
            .DoesArticleNameMatch(name);
        // Assert
        Assert.That(result, Is.True);
    }
}