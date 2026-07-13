using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace Core;

public static class Config
{
    private static readonly ConfigData Data = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build()
        .Get<ConfigData>()!;

    public static string Url
        => Data.Url;

    public static IWebDriver CreateDriver()
        => WebDriverFactory.CreateDriver(Data.Browser, Data.BrowserSettings);
}