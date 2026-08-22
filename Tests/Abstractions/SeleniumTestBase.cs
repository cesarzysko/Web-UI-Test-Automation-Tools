using Business;
using Core;
using log4net;
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
    }

    private static void HandleLogs()
    {
        var log = LogManager.GetLogger(typeof(SeleniumTestBase));

        var hierarchy = (Hierarchy?)log.Logger.Repository;
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

            if (string.IsNullOrWhiteSpace(fileAppender.File))
            {
                continue;
            }

            TestContext.AddTestAttachment(fileAppender.File, "Test log");
        }
    }
}