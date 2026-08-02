using System.Diagnostics;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Core;

public sealed class WebGestureController
    : IGestureController
{
    private readonly IWebDriver Driver;
    private readonly IElementFinder Finder;

    public WebGestureController(IWebDriver driver, IElementFinder finder)
    {
        Driver = driver;
        Finder = finder;
    }

    private static ILog Log => LogManager.GetLogger(typeof(WebGestureController));

    public void ScrollToElement(By locator)
    {
        const int TriesUntilStable = 5;
        const int TrySleepMs = 200;

        var js = (IJavaScriptExecutor)Driver;
        int previousHeight = -1;
        int currentTries = 0;
        Stopwatch sw = Stopwatch.StartNew();
        while (currentTries < TriesUntilStable)
        {
            int currentHeight = Convert.ToInt32(js.ExecuteScript("return document.scrollingElement.scrollHeight"));
            if (currentHeight == previousHeight)
            {
                currentTries++;
            }
            else
            {
                previousHeight = currentHeight;
                currentTries = 0;
            }

            Thread.Sleep(TrySleepMs);
        }

        sw.Stop();
        Log.InfoFormat("Scroll height stabilized after {0}ms.", sw.Elapsed.TotalMilliseconds);
        Log.InfoFormat("Scrolling down to web element with locator \"{0}\".", locator);
        var elem = Finder.Find(locator);
        js.ExecuteScript("arguments[0].scrollIntoView({block:'end'});", elem);
    }

    public void SwipeElementHorizontally(By locator, int by, int msDuration, int msPause)
    {
        Log.InfoFormat("Swiping web element with locator \"{0}\" horizontally by {1}px over {2}ms.", locator, by, msDuration);
        var elem = Finder.Find(locator);
        var pointer = new PointerInputDevice(PointerKind.Mouse);
        var sequence = new ActionSequence(pointer, 0);

        sequence.AddAction(pointer.CreatePointerMove(elem, 0, 0, TimeSpan.Zero));
        sequence.AddAction(pointer.CreatePointerDown(MouseButton.Left));

        var deltaTimeSpan = TimeSpan.FromMilliseconds(10);
        var deltaToTotal = deltaTimeSpan.TotalMilliseconds / msDuration;
        var byDelta = (int)(deltaToTotal * by);

        for (int current = 0; Math.Abs(current) < Math.Abs(by); current += byDelta)
        {
            sequence.AddAction(pointer.CreatePointerMove(CoordinateOrigin.Pointer, byDelta, 0, deltaTimeSpan));
        }

        sequence.AddAction(pointer.CreatePointerUp(MouseButton.Left));
        sequence.AddAction(pointer.CreatePointerMove(elem, 0, 0, TimeSpan.FromMilliseconds(msPause)));
        ((IActionExecutor)Driver).PerformActions([sequence]);
        Log.InfoFormat("Swiping web element with locator \"{0}\" completed.", locator);
    }
}