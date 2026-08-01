using OpenQA.Selenium;

namespace Core;

public interface IGestureController
{
    void ScrollToElement(By locator);
    void SwipeElementHorizontally(By locator, int by, int msDuration, int msPause);
}