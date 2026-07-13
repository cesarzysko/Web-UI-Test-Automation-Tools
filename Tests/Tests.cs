using Core;
using static Tests.Locators;

namespace Tests;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[TestFixture]
public class Tests
{
    private const int ImplicitWaitTimeInSeconds = 10;

    private WebDriverHelper Driver;

    [SetUp]
    public void SetUp()
    {
        var logger = new TextWriterLogger(TestContext.Out);
        Driver = new WebDriverHelper(Config.CreateDriver, logger);
        Driver.SetImplicitWaitInSeconds(ImplicitWaitTimeInSeconds);
        Driver.NavigateToUrl(Config.Url);
    }

    [TearDown]
    public void TearDown()
    {
        ((IDisposable)Driver).Dispose();
    }

    [TestCase("C++", Country.Poland)]
    [TestCase("JavaScript", Country.Mexico)]
    [TestCase("Python", Country.Portugal)]
    public void PositionSearch_WithCriteria_ProgrammingLanguagePresentInDescription(
        string programmingLanguage,
        Country country)
    {
        Driver.Click(CareersBtnLocator);
        Driver.Click(StartYourSearchBtnLocator);
        Driver.SendKeysWithEnter(RoleOrKeywordInputLocator, programmingLanguage);
        Driver.SendKeysWithEnter(CountryInputLocator, country.ToString());
        Driver.Click(RemoteCheckboxLocator, ClickCookies);
        Driver.Click(PositionSearchBtnLocator);
        Driver.Click(LatestResultExpanderLocator);
        bool result = Driver.DoesContainText(LatestResultDescriptionLocator, programmingLanguage);
        Assert.That(result, Is.True);
    }

    [TestCase("BLOCKCHAIN")]
    [TestCase("Cloud")]
    [TestCase("Automation")]
    public void GlobalSearch_WithWord_AllLinkTextsContainWord(
        string input)
    {
        Driver.Click(MagnifierBtnLocator);
        Driver.SendKeys(SearchInputLocator, input);
        Driver.Click(SearchBtnLocator);
        bool result = Driver.DoAllContainText(ArticlesLocator, input);
        Assert.That(result, Is.True);
    }

    private void ClickCookies()
    {
        Driver.Click(CookiesBtnLocator);
    }
}