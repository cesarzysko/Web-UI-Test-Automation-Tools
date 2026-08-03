using OpenQA.Selenium;

namespace Core;

public sealed class WebNavigator
    : INavigator
{
    private readonly IWebDriver Driver;

    public WebNavigator(IWebDriver driver)
    {
        Driver = driver;
    }

    void INavigator.NavigateToUrl(string url)
    {
        Driver.Navigate().GoToUrl(url);
    }
}