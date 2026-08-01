using OpenQA.Selenium;

namespace Core;

public interface IBrowserFactory
{
    IWebDriver CreateDriver();
}