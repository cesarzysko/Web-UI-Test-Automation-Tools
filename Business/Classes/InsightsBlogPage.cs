using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class InsightsBlogPage
    : PageBase
{
    public InsightsBlogPage(IWebDriverWrapper driver)
        : base(driver) { }

    public string GetMatchingArticleName(string nameMatch)
    {
        var strippedName = nameMatch.Replace(" ", "").Replace("\u00A0", "");
        var containsNameConstraint =  // language=XPath
            $"contains(translate(string(.), ' \u00A0', ''), '{strippedName}')";
        By locator = // language=XPath
            By.XPath($"//main//descendant::*[{containsNameConstraint} and not(descendant::*[{containsNameConstraint}])]");
        try
        {
            Log.Info("Trying to read the article name.");
            var name = Driver.GetText(locator);
            Log.InfoFormat("Found article name \"{0}\".", name);
            return name;
        }
        catch (NoSuchElementException)
        {
            Log.WarnFormat("Could not find an article name using the locator \"{0}\".", locator);
            return string.Empty;
        }
    }
}