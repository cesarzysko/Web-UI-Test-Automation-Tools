using OpenQA.Selenium;

namespace Core;

public interface IWebDriverFactory
{
    public Browser Browser { get; }

    public IWebDriver CreateDriver();
}