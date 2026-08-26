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
        HandleScreenshotAttachment();
        base.TearDown();
    }

    private void HandleScreenshotAttachment()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
        {
            return;
        }

        var path = ScreenshotTaker.TakeScreenshot(TestContext.CurrentContext.Test.FullName);
        TestContext.AddTestAttachment(path, "Failure screenshot");
        TestContext.Out.WriteLine($"[[ATTACHMENT|{path}]]");
    }
}