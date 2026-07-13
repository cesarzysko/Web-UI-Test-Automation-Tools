using Business;
using Core;

namespace Tests;

[Parallelizable(ParallelScope.All)]
[TestFixture]
public class Tests
{
    private IConfig Config { get; } = new JsonFileConfig();

    private const int ImplicitWaitTimeInSeconds = 10;

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

    private IWebDriverWrapper GetDriver()
    {
        var logger = new TextWriterLogger(TestContext.Out);
        var driver = WebDriverFactory.CreateDriver(Config.Data);
        var driverWrapper = new WebDriverWrapper(driver, logger);
        driverWrapper.SetImplicitWaitInSeconds(ImplicitWaitTimeInSeconds);
        return driverWrapper;
    }
}