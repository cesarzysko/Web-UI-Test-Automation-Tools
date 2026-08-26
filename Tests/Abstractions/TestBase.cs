using Core;
using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public abstract class TestBase
{
    protected IServiceScope TestScope { get; private set; }

    protected ILog Log => LogManager.GetLogger(GetType());

    [SetUp]
    public virtual void SetUp()
    {
        TestScope = GlobalTestSetup.ServiceProvider.CreateScope();
        SetUpLogging();
    }

    [TearDown]
    public virtual void TearDown()
    {
        HandleLogsAttachment();
        TestScope.Dispose();
        TearDownLogging();
    }

    private static void SetUpLogging()
    {
        var testName = FileNameSanitizer.Sanitize(TestContext.CurrentContext.Test.Name);
        var testExecutionId = $"{testName}__{DateTime.Now:yyyy-MM-dd__HH-mm-ss-fff}";
        LogicalThreadContext.Properties["TestId"] = testExecutionId;
    }

    private static void TearDownLogging()
    {
        LogicalThreadContext.Properties.Remove("TestId");
    }

    private void HandleLogsAttachment()
    {
        var hierarchy = (Hierarchy?)Log.Logger.Repository;
        if (hierarchy == null)
        {
            return;
        }

        var testId = LogicalThreadContext.Properties["TestId"] as string;
        if (string.IsNullOrWhiteSpace(testId))
        {
            return;
        }

        var perTestAppender = hierarchy.GetAppenders()
            .OfType<PerTestFileAppender>()
            .FirstOrDefault();

        var path = perTestAppender?.GetLogFilePath(testId);
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        TestContext.AddTestAttachment(path, "Test log");
        TestContext.Out.WriteLine($"[[ATTACHMENT|{path}]]");
    }
}