using Business;
using Core;
using Core.REST;
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
        AddRest(sc);
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
        sc.AddTransient<IElementFinder, WebElementFinder>();
        sc.AddTransient<IDownloadWaiter, WebDownloadWaiter>();
        sc.AddTransient<IElementInteractor, WebElementInteractor>();
        sc.AddTransient<IGestureController, WebGestureController>();
        sc.AddTransient<INavigator, WebNavigator>();
        sc.AddTransient<IScreenshotTaker, WebScreenshotTaker>(sp =>
        {
            var subDir = sp.GetRequiredService<IConfig>().Data.ScreenshotsSubDirectory;
            return ActivatorUtilities.CreateInstance<WebScreenshotTaker>(sp, subDir);
        });
    }

    private static void AddPageObjects(ServiceCollection sc)
    {
        sc.AddScoped<IPageFactory, PageFactory>(sp => new PageFactory(sp));
        sc.AddTransient<HomePage>(sp =>
        {
            var url = sp.GetRequiredService<IConfig>().Data.MainPageUrl;
            return ActivatorUtilities.CreateInstance<HomePage>(sp, url);
        });
        sc.AddTransient<CareersPage>();
        sc.AddTransient<HomeCareersPage>();
        sc.AddTransient<HomeFooterWidget>();
        sc.AddTransient<HomeInsightsPage>();
        sc.AddTransient<HomeSearchResultsPage>();
        sc.AddTransient<HomeSearchWidget>();
        sc.AddTransient<InsightsBlogPage>();
    }

    private static void AddRest(ServiceCollection sc)
    {
        sc.AddSingleton<ApiSettings>(sp => sp.GetRequiredService<IConfig>().Data.ApiSettings);
        sc.AddScoped<ApiClient>();
    }
}