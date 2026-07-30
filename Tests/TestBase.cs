using Business;
using Core;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using ILogger = Microsoft.Extensions.Logging.ILogger;

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
        serviceProvider = BuildServiceProvider();
        ConfigureLogging();
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

    private static ServiceProvider BuildServiceProvider()
    {
        var sc = new ServiceCollection();
        sc.AddSingleton<IConfig, ConfigurationFileConfig>();
        sc.AddScoped<ILogger, TextWriterLogger>(_ => new TextWriterLogger(TestContext.Out));
        sc.AddScoped<IDownloadPathGetter, DownloadPathGetter>();
        sc.AddScoped<WebDriverFactory>();
        sc.AddScoped<IWebDriver>(sp => sp.GetRequiredService<WebDriverFactory>().CreateDriver());
        sc.AddScoped<IWebDriverWrapper, WebDriverWrapper>();
        sc.AddScoped<HomePage>();
        return sc.BuildServiceProvider();
    }

    private static void ConfigureLogging()
    {
        var configData = serviceProvider.GetRequiredService<IConfig>().Data;
        var hierarchy = (Hierarchy)LogManager.GetRepository();
        hierarchy.ResetConfiguration();
        XmlConfigurator.Configure(hierarchy, new FileInfo("Config/log4net.config"));
        hierarchy.Root.Level = Level.All;
        hierarchy.HandleConsoleOutput(configData.LoggingSettings.ConsoleOutput);
        hierarchy.HandleFileOutput(configData.LoggingSettings.FileOutput);
        hierarchy.Configured = true;
    }
}