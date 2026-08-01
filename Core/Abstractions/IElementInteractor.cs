using OpenQA.Selenium;

namespace Core;

public interface IElementInteractor
{
    string GetText(By locator);
    IReadOnlyList<string> GetTexts(By locator);
    int GetElementWidth(By locator);
    void Click(By locator);
    void ClickJS(By locator);
    void ClickWithInterceptFallback(By locator, Action onClickIntercepted);
    void SendKeysWithEnter(By locator, string input);
    void SendKeys(By locator, string input);
}