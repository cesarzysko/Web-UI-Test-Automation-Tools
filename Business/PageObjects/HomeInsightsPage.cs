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

    private readonly IGestureController GestureController;

    public HomeInsightsPage(IElementInteractor interactor, IGestureController gestureController)
        : base(interactor)
    {
        GestureController = gestureController;
    }

    public HomeInsightsPage SwipeCarousel(int swipes)
    {
        const float SwipeLengthRatio = 0.4f;
        var elemWidth = Interactor.GetElementWidth(ArticleLocator);
        var swipeLength = (int)(elemWidth * SwipeLengthRatio);
        Log.InfoFormat("Swiping the carousel \"{0}\" times. Swipe length will be \"{1}\".", swipes, swipeLength);
        for (int i = 0; i < swipes; ++i)
        {
            GestureController.SwipeElementHorizontally(CarouselLocator, -swipeLength, SwipeDurationMs, AfterSwipePauseMs);
        }

        return this;
    }

    public string GetCurrentArticleName()
    {
        Log.Info("Reading the text from the current article in the carousel.");
        string name = Interactor.GetText(ArticleNameLocator);
        Log.InfoFormat("Text from the current article in the carousel: \"{0}\".", name);
        return name;
    }

    public void ClickReadMoreButtonForCurrentArticle()
    {
        Log.Info("Clicking the \"Read More\" button for the current article on the carousel.");
        Interactor.Click(ArticleReadMoreBtnLocator);
    }
}