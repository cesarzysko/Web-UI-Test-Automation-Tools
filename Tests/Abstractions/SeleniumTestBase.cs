using Business;
using Core;
using log4net.Appender;
using log4net.Repository.Hierarchy;
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
        HandleScreenshot();
        HandleLogs();

        base.TearDown();
    }

    private void HandleScreenshot()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
        {
            return;
        }

        var path = ScreenshotTaker.TakeScreenshot(TestContext.CurrentContext.Test.FullName);
        TestContext.AddTestAttachment(path, "Failure screenshot");
        TestContext.Out.WriteLine($"[[Attachment|{path}]]");
    }

    private void HandleLogs()
    {
        var hierarchy = (Hierarchy?)Log.Logger.Repository;
        if (hierarchy == null)
        {
            return;
        }

        foreach (IAppender appender in hierarchy.GetAppenders())
        {
            if (appender is not FileAppender fileAppender)
            {
                continue;
            }

            string? path = fileAppender.File;
            if (string.IsNullOrWhiteSpace(path))
            {
                continue;
            }

            TestContext.AddTestAttachment(path, "Test log");
            TestContext.Out.WriteLine($"[[Attachment|{path}]]");
        }
    }
}