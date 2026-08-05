using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class ServicePage
    : PageBase
{
    private static readonly By TitleLocator = // language=XPath
        By.XPath("//main//p[contains(@class, 'scaling-of-text-wrapper')]");

    private static readonly By RelatedExpertiseLocator = // language=XPath
        By.XPath("//main//span[contains(text(), 'Our Related Expertise')]");

    public ServicePage(IElementInteractor interactor)
        : base(interactor) { }

    public string GetTitle()
    {
        return Interactor.GetText(TitleLocator);
    }

    public bool IsRelatedExpertisePresent()
    {
        return Interactor.Exists(RelatedExpertiseLocator);
    }
}