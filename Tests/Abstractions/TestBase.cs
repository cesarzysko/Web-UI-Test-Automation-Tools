using Business;
using Core;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Interfaces;
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
        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            Driver.TakeScreenshot(TestContext.CurrentContext.Test.FullName);
        }

        testScope.Dispose();
    }

    private static ServiceProvider BuildServiceProvider()
    {
        var sc = new ServiceCollection();
        sc.AddSingleton<IConfig, ConfigurationFileConfig>();
        sc.AddScoped<IDownloadPathGetter, DownloadPathGetter>();
        sc.AddScoped<IWebDriverFactory, ChromeDriverFactory>();
        sc.AddScoped<IBrowserFactory, BrowserFactory>();
        sc.AddScoped<IWebDriver>(sp => sp.GetRequiredService<IBrowserFactory>().CreateDriver());
        sc.AddScoped<IWebDriverWrapper, WebDriverWrapper>();
        sc.AddScoped<HomePage>();
        return sc.BuildServiceProvider();
    }

    private static void ConfigureLogging()
    {
        var loggingSettings = serviceProvider.GetRequiredService<IConfig>().Data.LoggingSettings;
        var hierarchy = (Hierarchy)LogManager.GetRepository();
        hierarchy.ResetConfiguration();
        XmlConfigurator.Configure(hierarchy, new FileInfo("Config/log4net.config"));
        hierarchy.Root.Level = loggingSettings.LogLevel.ToLog4NetLevel();
        hierarchy.HandleConsoleOutput(loggingSettings.ConsoleOutput);
        hierarchy.HandleFileOutput(loggingSettings.FileOutput);
        hierarchy.Configured = true;
    }
}