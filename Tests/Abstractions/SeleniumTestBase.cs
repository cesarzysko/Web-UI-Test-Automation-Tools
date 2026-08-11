using Business;
using Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;

namespace Tests;

public abstract class SeleniumTestBase
    : TestBase
{
    protected HomePage HomePage
        => TestScope.ServiceProvider.GetRequiredService<IPageFactory>().Create<HomePage>();
    protected IDownloadWaiter DownloadWaiter
        => TestScope.ServiceProvider.GetRequiredService<IDownloadWaiter>();

    private IScreenshotTaker ScreenshotTaker
        => TestScope.ServiceProvider.GetRequiredService<IScreenshotTaker>();

    [TearDown]
    public override void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            ScreenshotTaker.TakeScreenshot(TestContext.CurrentContext.Test.FullName);
        }

        base.TearDown();
    }
}