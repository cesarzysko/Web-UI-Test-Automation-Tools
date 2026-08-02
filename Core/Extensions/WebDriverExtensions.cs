using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core;

public static class WebDriverExtensions
{
    extension(IWebDriver webDriver)
    {
        public void SetImplicitWait(TimeSpan timeSpan)
        {
            webDriver.Manage().Timeouts().ImplicitWait = timeSpan;
        }

        public TimeSpan GetImplicitWait()
        {
            return webDriver.Manage().Timeouts().ImplicitWait;
        }

        public WebDriverWait GetExplicitWait(TimeSpan timeSpan)
        {
            return new WebDriverWait(webDriver, timeSpan);
        }
    }
}