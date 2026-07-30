using OpenQA.Selenium;

namespace Core.Abstractions;

public interface IWebDriverFactory
{
    public Browser Browser { get; }

    public IWebDriver CreateDriver();
}