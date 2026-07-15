using OpenQA.Selenium.Chrome;

namespace Core;

public static class ChromeOptionsExtensions
{
    extension(ChromeOptions options)
    {
        public void HandleMaximized(bool maximized)
        {
            if (maximized)
            {
                options.AddArguments("start-maximized");
            }
        }

        public void HandleDownloads(string downloadPath)
        {
            options.AddUserProfilePreference("download.default_directory", downloadPath);
        }
    }
}