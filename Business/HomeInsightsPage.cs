using Core;
using OpenQA.Selenium;

namespace Business;

public sealed class HomeInsightsPage
    : PageBase
{
    private const int SwipeDurationMs = 250;
    private const int AfterSwipePauseMs = 500;

    private static readonly By CarouselLocator =
        By.ClassName("owl-stage");

    private static readonly By ArticleLocator =
        By.ClassName("owl-item");

    private static readonly By ArticleNameLocator = // language=CSS
        By.CssSelector("div.owl-item.active div.text");

    private static readonly By ArticleReadMoreBtnLocator = // language=CSS
        By.CssSelector("div.owl-item.active a.custom-link");

    public HomeInsightsPage(IWebDriverWrapper driver)
        : base(driver) { }

    public HomeInsightsPage SwipeCarousel(int swipes)
    {
        var elemWidth = Driver.GetElementWidth(ArticleLocator);
        var swipeLength = (int)(elemWidth * 0.4f);
        for (int i = 0; i < swipes; ++i)
        {
            Driver.SwipeElementHorizontally(CarouselLocator, -swipeLength, SwipeDurationMs, AfterSwipePauseMs);
        }

        return this;
    }

    public HomeInsightsPage GetCurrentArticleName(out string name)
    {
        name = Driver.GetText(ArticleNameLocator);
        return this;
    }

    public InsightsBlogPage ClickReadMoreButtonForCurrentArticle()
    {
        Driver.Click(ArticleReadMoreBtnLocator);
        return new InsightsBlogPage(Driver);
    }
}