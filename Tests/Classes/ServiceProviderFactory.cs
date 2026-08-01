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
        AddConfiguration(sc);
        AddWebDriver(sc);
        AddPageObjects(sc);
        return sc.BuildServiceProvider();
    }

    private static void AddConfiguration(ServiceCollection sc)
    {
        sc.AddSingleton<IConfig, ConfigurationFileConfig>();
        sc.AddSingleton<LoggingSettings>(sp => sp.GetRequiredService<IConfig>().Data.LoggingSettings);
        sc.AddSingleton<ILog4NetConfigurator, Log4NetConfigurator>();
    }

    private static void AddWebDriver(ServiceCollection sc)
    {
        sc.AddScoped<IDownloadPathGetter, DownloadPathGetter>();
        sc.AddScoped<IWebDriverFactory, ChromeDriverFactory>();
        sc.AddScoped<IBrowserFactory, BrowserFactory>();
        sc.AddScoped<IWebDriver>(sp => sp.GetRequiredService<IBrowserFactory>().CreateDriver());
        sc.AddScoped<WebDriverWrapper>();
        sc.AddScoped<IDownloadWaiter>(sp => sp.GetRequiredService<WebDriverWrapper>());
        sc.AddScoped<IElementInteractor>(sp => sp.GetRequiredService<WebDriverWrapper>());
        sc.AddScoped<IGestureController>(sp => sp.GetRequiredService<WebDriverWrapper>());
        sc.AddScoped<INavigator>(sp => sp.GetRequiredService<WebDriverWrapper>());
        sc.AddScoped<IScreenshotTaker>(sp => sp.GetRequiredService<WebDriverWrapper>());
    }

    private static void AddPageObjects(ServiceCollection sc)
    {
        sc.AddScoped<IPageFactory, PageFactory>(sp => new PageFactory(sp));
        sc.AddTransient<HomePage>();
        sc.AddTransient<CareersPage>();
        sc.AddTransient<HomeCareersPage>();
        sc.AddTransient<HomeFooterWidget>();
        sc.AddTransient<HomeInsightsPage>();
        sc.AddTransient<HomeSearchResultsPage>();
        sc.AddTransient<HomeSearchWidget>();
        sc.AddTransient<InsightsBlogPage>();
    }
}