using OpenQA.Selenium;

namespace Core;

public interface IWebDriverWrapper
    : IDisposable
{
    void NavigateToUrl(string url);
    string GetText(By locator);
    IReadOnlyList<string> GetTexts(By locator);
    int GetElementWidth(By locator);
    void Click(By locator);
    void ClickJS(By locator);
    void ClickWithInterceptFallback(By locator, Action onClickIntercepted);
    void ScrollToElement(By locator);
    void SwipeElementHorizontally(By locator, int by, int msDuration, int msPause);
    void SendKeysWithEnter(By locator, string input);
    void SendKeys(By locator, string input);
    bool IsFileDownloaded(string fileName, TimeSpan timeout);
}