using Business;
using Core;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace Tests;

public static class ServiceProviderFactory
{
    public static ServiceProvider CreateProvider()
    {
        var sc = new ServiceCollection();
        sc.AddSingleton<IConfig, ConfigurationFileConfig>();
        sc.AddSingleton<LoggingSettings>(sp => sp.GetRequiredService<IConfig>().Data.LoggingSettings);
        sc.AddSingleton<ILog4NetConfigurator, Log4NetConfigurator>();
        sc.AddScoped<IDownloadPathGetter, DownloadPathGetter>();
        sc.AddScoped<IWebDriverFactory, ChromeDriverFactory>();
        sc.AddScoped<IBrowserFactory, BrowserFactory>();
        sc.AddScoped<IWebDriver>(sp => sp.GetRequiredService<IBrowserFactory>().CreateDriver());
        sc.AddScoped<IWebDriverWrapper, WebDriverWrapper>();
        sc.AddScoped<HomePage>();
        return sc.BuildServiceProvider();
    }
}