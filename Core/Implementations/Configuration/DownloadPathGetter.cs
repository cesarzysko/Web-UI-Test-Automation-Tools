namespace Core;

public sealed class DownloadPathGetter
    : IDownloadPathGetter
{
    private readonly string DownloadPath = Directory.CreateTempSubdirectory(GetNewDownloadDirectory()).FullName;

    public string GetDownloadPath()
    {
        return DownloadPath;
    }

    private static string GetNewDownloadDirectory()
    {
        return Guid.NewGuid().ToString();
    }
}