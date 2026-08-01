using OpenQA.Selenium;

namespace Core;

public static class WebDriverExtensions
{
    extension(IWebDriver webDriver)
    {
        public void SetImplicitWait(TimeSpan timeSpan)
        {
            webDriver.Manage().Timeouts().ImplicitWait = timeSpan;
        }
    }
}