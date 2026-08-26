using OpenQA.Selenium.Firefox;

namespace Core;

public static class FirefoxOptionsExtensions
{
    extension(FirefoxOptions options)
    {
        public void HandleDownloads(string downloadPath)
        {
            options.SetPreference("browser.download.dir", downloadPath);
        }
    }
}