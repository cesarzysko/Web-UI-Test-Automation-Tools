using OpenQA.Selenium;

namespace Core;

public interface IWebDriverWrapper
    : IDisposable
{
    void NavigateToUrl(string url);
    void SetImplicitWaitInSeconds(float seconds);
    bool DoAllContainText(By locator, string text);
    bool DoesContainText(By locator, string text);
    void Click(By locator);
    void ClickJS(By locator);
    void ClickWithInterceptFallback(By locator, Action onClickIntercepted);
    void ScrollToElement(By locator);
    void SendKeysWithEnter(By locator, string input);
    void SendKeys(By locator, string input);
    bool IsFileDownloaded(string fileName, TimeSpan timeout);
}