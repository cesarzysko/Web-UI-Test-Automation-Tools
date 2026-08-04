using Core;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using TechTalk.SpecFlow;

namespace Tests.StepDefinitions;

[Binding]
public sealed class Hooks
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        return ServiceProviderFactory.CreateCollection();
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var config = new ConfigurationFileConfig();
        ((ILog4NetConfigurator)new Log4NetConfigurator(config.Data.LoggingSettings)).Configure();
    }

    [AfterScenario]
    public static void AfterScenario(ScenarioContext ctx, IScreenshotTaker screenshotTaker)
    {
        if (ctx.TestError != null)
        {
            screenshotTaker.TakeScreenshot(ctx.ScenarioInfo.Title);
        }
    }
}