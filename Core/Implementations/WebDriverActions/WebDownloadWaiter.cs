using System.Diagnostics;
using log4net;

namespace Core;

public sealed class WebDownloadWaiter
    : IDownloadWaiter
{
    private static readonly string[] TempFileNames = [
        ".org.chromium.Chromium",
        ".crdownload",
        ".tmp"
    ];

    private static readonly TimeSpan DownloadSleepTime = TimeSpan.FromMilliseconds(50);

    private readonly string DownloadPath;

    public WebDownloadWaiter(IDownloadPathGetter downloadPathGetter)
    {
        DownloadPath = downloadPathGetter.GetDownloadPath();
    }

    private static ILog Log => LogManager.GetLogger(typeof(WebDownloadWaiter));

    private static void LogWaitingForFile(string downloadPath)
    {
        Log.InfoFormat("Waiting for file download at path \"{0}\".", downloadPath);
    }

    private static void LogFileFound(string fileName, double totalSeconds)
    {
        Log.InfoFormat("File \"{0}\" was found after waiting for {1} seconds.", fileName, totalSeconds);
    }

    private static void LogFileNotFound(double totalSeconds)
    {
        Log.WarnFormat("No file was not found after waiting for {0} seconds.", totalSeconds);
    }

    string IDownloadWaiter.GetDownloadedFileName(TimeSpan timeout)
    {
        Stopwatch sw = Stopwatch.StartNew();
        LogWaitingForFile(DownloadPath);
        while (sw.Elapsed < timeout)
        {
            Thread.Sleep(DownloadSleepTime);
            var files = GetDownloadedFileNames();
            if (files.Count == 0)
            {
                continue;
            }

            string fileName = Path.GetFileName(files[0]);
            LogFileFound(fileName, sw.Elapsed.TotalSeconds);

            return fileName;
        }

        LogFileNotFound(timeout.TotalSeconds);
        return string.Empty;
    }

    private IReadOnlyList<string> GetDownloadedFileNames()
    {
        return Directory.GetFiles(DownloadPath).WhereNotContainsAny(TempFileNames);
    }
}