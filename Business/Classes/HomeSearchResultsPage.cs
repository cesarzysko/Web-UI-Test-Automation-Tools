using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeSearchResultsPage
    : PageBase
{
    private static readonly By ArticlesLocator = // language=CSS
        By.CssSelector("div.search-results__items article");

    private readonly IElementInteractor Interactor;

    public HomeSearchResultsPage(IElementInteractor interactor)
    {
        Interactor = interactor;
    }

    public IReadOnlyList<string> GetAllResults()
    {
        Log.Info("Reading all texts from the current search results.");
        var texts = Interactor.GetTexts(ArticlesLocator);
        Log.InfoFormat("Texts from the current search results: \"{0}\".", string.Join(", ", texts));
        return texts;
    }
}