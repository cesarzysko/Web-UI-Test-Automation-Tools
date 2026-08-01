using OpenQA.Selenium;

namespace Core;

public sealed class BrowserFactory
{
    private readonly IWebDriverFactory factory;

    public BrowserFactory(IEnumerable<IWebDriverFactory> webDriverFactories, IConfig config)
    {
        factory = webDriverFactories.First(f => f.Browser == config.Data.Browser);
    }

    public IWebDriver CreateDriver()
    {
        return factory.CreateDriver();
    }
}