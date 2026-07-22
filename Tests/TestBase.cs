using System.Diagnostics.CodeAnalysis;
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

    [SuppressMessage(
        "Structure",
        "NUnit1032:An IDisposable field/property should be Disposed in a TearDown method",
        Justification = "Disposed by DI scope in TearDown.")]
    protected IWebDriverWrapper Driver
    {
        get;
        private set => field = value ?? throw new ArgumentNullException(nameof(value));
    } = null!;

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
        Driver = testScope.ServiceProvider.GetRequiredService<IWebDriverWrapper>();
    }

    [TearDown]
    public void TearDown()
    {
        testScope.Dispose();
    }
}