using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class AIServiceSelectorCtx
    : PageBase
{
    public AIServiceSelectorCtx(IElementInteractor interactor)
        : base(interactor) { }

    public void ClickMatchingService(string name)
    {
        var formattedName = name.ToLower().Replace(' ', '-');
        By locator = // language=XPath
            By.XPath($"//a[contains(@href, '/services/artificial-intelligence')]//following-sibling::ul//a[contains(@href, '{formattedName}')]");
        Log.InfoFormat("Clicking the \"{0}\" service button.", name);
        Interactor.Click(locator);
    }
}