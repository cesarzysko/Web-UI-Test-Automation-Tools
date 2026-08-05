using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class ServiceSelectorCtx
    : PageBase
{
    public ServiceSelectorCtx(IElementInteractor interactor)
        : base(interactor) { }

    public void ClickMatchingService(string name)
    {
        var formattedName = name.ToLower().Replace(' ', '-');
        By locator = // language=XPath
            By.XPath($"//a[contains(@href, '{formattedName}')]");
        Log.InfoFormat("Clicking the \"{0}\" service button.", name);
        Interactor.Click(locator);
    }
}