using Business;
using Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

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
        var sc = new ServiceCollection();
        sc.AddSingleton<IConfig, ConfigurationFileConfig>();
        sc.AddScoped<ILogger, TextWriterLogger>(_ => new TextWriterLogger(TestContext.Out));
        sc.AddScoped<IDownloadPathGetter, DownloadPathGetter>();
        sc.AddScoped<WebDriverFactory>();
        sc.AddScoped<IWebDriver>(sp => sp.GetRequiredService<WebDriverFactory>().CreateDriver());
        sc.AddScoped<IWebDriverWrapper, WebDriverWrapper>();
        sc.AddScoped<HomePage>();
        serviceProvider = sc.BuildServiceProvider();
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
        testScope.Dispose();
    }
}