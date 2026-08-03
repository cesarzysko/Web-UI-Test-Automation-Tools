using OpenQA.Selenium;

namespace Core;

public sealed class BrowserFactory
    : IBrowserFactory
{
    private readonly IWebDriverFactory Factory;

    public BrowserFactory(IEnumerable<IWebDriverFactory> webDriverFactories, IConfig config)
    {
        Factory = webDriverFactories.First(f => f.Browser == config.Data.Browser);
    }

    public IWebDriver CreateDriver()
    {
        return Factory.CreateDriver();
    }
}