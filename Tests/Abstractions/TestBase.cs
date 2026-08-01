using Business;
using Core;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;

namespace Tests;

public abstract class TestBase
{
    private static ServiceProvider serviceProvider;

    private IServiceScope testScope;

    protected HomePage HomePage =>
        testScope.ServiceProvider.GetRequiredService<HomePage>();

    protected IWebDriverWrapper Driver =>
        testScope.ServiceProvider.GetRequiredService<IWebDriverWrapper>();

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        serviceProvider = ServiceProviderFactory.CreateProvider();
        serviceProvider.GetRequiredService<ILog4NetConfigurator>().Configure();
    }

    [OneTimeTearDown]
    public static void OneTimeTearDown()
    {
        serviceProvider.Dispose();
    }

    [SetUp]
    public void SetUp()
    {
        testScope = serviceProvider.CreateScope();
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            Driver.TakeScreenshot(TestContext.CurrentContext.Test.FullName);
        }

        testScope.Dispose();
    }
}