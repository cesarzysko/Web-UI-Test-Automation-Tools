using log4net;

namespace Core;

public sealed class DownloadPathGetter
    : IDownloadPathGetter
{
    private string? downloadPath;

    public string GetDownloadPath()
    {
        LogManager.GetLogger(nameof(DownloadPathGetter)).Info("INSTANTIATING DOWNLOAD PATH GETTER");
        downloadPath ??= Directory.CreateTempSubdirectory(GetNewDownloadDirectory()).FullName;
        return downloadPath;
    }

    private static string GetNewDownloadDirectory()
    {
        return Guid.NewGuid().ToString();
    }
}