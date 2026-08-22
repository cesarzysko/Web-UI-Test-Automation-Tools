using log4net;
using OpenQA.Selenium;

namespace Core;

public sealed class WebScreenshotTaker
    : IScreenshotTaker
{
    private readonly IWebDriver Driver;
    private readonly string ScreenshotPath;

    public WebScreenshotTaker(IWebDriver driver, string screenshotsSubDirectory)
    {
        Driver = driver;
        ScreenshotPath = Path.Combine(Directory.GetCurrentDirectory(), screenshotsSubDirectory);
    }

    private static ILog Log => LogManager.GetLogger(typeof(WebScreenshotTaker));

    string IScreenshotTaker.TakeScreenshot(string name)
    {
        var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
        var filePath = GetScreenshotPath(name);
        Log.InfoFormat("Saving screenshot at \"{0}\".", filePath);
        screenshot.SaveAsFile(filePath);
        return filePath;
    }

    private string GetScreenshotPath(string name)
    {
        var fileName = GetScreenshotFileName(name);
        return Path.Combine(ScreenshotPath, fileName);
    }

    private static string GetScreenshotFileName(string name)
    {
        var now = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-fff");
        return $"{now}_{name}";
    }
}