using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class InsightsBlogPage
    : PageBase
{
    public InsightsBlogPage(IElementInteractor interactor)
        : base(interactor) { }

    public string GetMatchingArticleName(string nameMatch)
    {
        var locator = GetNameMatchLocator(nameMatch);
        try
        {
            Log.Info("Trying to read the article name.");
            var name = Interactor.GetText(locator);
            Log.InfoFormat("Found article name \"{0}\".", name);
            return name;
        }
        catch (NoSuchElementException)
        {
            Log.WarnFormat("Could not find an article name using the locator \"{0}\".", locator);
            return string.Empty;
        }
    }

    private static By GetNameMatchLocator(string nameMatch)
    {
        var containsNameConstraint = GetContainsStringConstraint(nameMatch);
        return // language=XPath
            By.XPath($"//main//descendant::*[{containsNameConstraint} and not(descendant::*[{containsNameConstraint}])]");
    }

    private static string GetContainsStringConstraint(string nameMatch)
    {
        var strippedName = GetStrippedName(nameMatch);
        return // language=XPath
            $"contains(translate(string(.), ' \u00A0', ''), '{strippedName}')";
    }

    private static string GetStrippedName(string nameMatch)
    {
        return nameMatch.Replace(" ", "").Replace("\u00A0", "");
    }
}