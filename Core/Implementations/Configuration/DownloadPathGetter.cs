using log4net;

namespace Core;

public sealed class DownloadPathGetter
    : IDownloadPathGetter
{
    private readonly string DownloadPath;

    public DownloadPathGetter()
    {
        LogManager.GetLogger(nameof(DownloadPathGetter)).Info("INSTANTIATING DOWNLOAD PATH GETTER");
        DownloadPath = Directory.CreateTempSubdirectory(GetNewDownloadDirectory()).FullName;
    }

    public string GetDownloadPath()
    {
        return DownloadPath;
    }

    private static string GetNewDownloadDirectory()
    {
        return Guid.NewGuid().ToString();
    }
}