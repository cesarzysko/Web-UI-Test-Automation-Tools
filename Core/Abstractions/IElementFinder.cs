using OpenQA.Selenium;

namespace Core;

public interface IElementFinder
{
    IWebElement Find(By locator);
    IReadOnlyList<IWebElement> FindAll(By locator);
}