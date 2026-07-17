using Business;
using Core;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace Tests;

[Parallelizable(ParallelScope.All)]
[TestFixture]
public sealed class Tests
{
    private const int ImplicitWaitTimeInSeconds = 5;

    private static readonly TimeSpan DownloadTimeout = TimeSpan.FromSeconds(5);
    private static readonly IConfig Config = new ConfigurationFileConfig();

    [TestCase("C++", Country.Poland)]
    [TestCase("JavaScript", Country.Mexico)]
    [TestCase("Python", Country.Portugal)]
    public void PositionSearch_WithCriteria_ProgrammingLanguagePresentInDescription(
        string programmingLanguage,
        Country country)
    {
        // Arrange
        using var driver = GetDriver();
        var homePage = new HomePage(driver, Config.Data.MainPageUrl);
        // Act
        var result = homePage
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
        // Arrange
        using var driver = GetDriver();
        var homePage = new HomePage(driver, Config.Data.MainPageUrl);
        // Act
        var result = homePage
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
        // Arrange
        using var driver = GetDriver();
        var homePage = new HomePage(driver, Config.Data.MainPageUrl);
        // Act
        homePage
            .GoToFooter()
            .ClickCodeOfEthicalConductButton();
        var result = driver.IsFileDownloaded(fileName, DownloadTimeout);
        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void ArticleCarousel_ReadMore_ArticleNameMatchesCarousel(
        [Range(0, 4)] int carouselSwipes)
    {
        // Arrange
        using var driver = GetDriver();
        var homePage = new HomePage(driver, Config.Data.MainPageUrl);
        // Act
        var result = homePage
            .ClickInsightsButton()
            .SwipeCarousel(carouselSwipes)
            .GetCurrentArticleName(out string name)
            .ClickReadMoreButtonForCurrentArticle()
            .DoesArticleNameMatch(name);
        // Assert
        Assert.That(result, Is.True);
    }

    private static IWebDriverWrapper GetDriver()
    {
        ILogger logger = new TextWriterLogger(TestContext.Out);
        string downloadPath = DownloadUtils.GetNewDownloadPath();
        IWebDriver driver = WebDriverFactory.CreateDriver(Config.Data, downloadPath);
        IWebDriverWrapper driverWrapper = new WebDriverWrapper(driver, logger, downloadPath);
        driverWrapper.SetImplicitWaitInSeconds(ImplicitWaitTimeInSeconds);
        return driverWrapper;
    }
}