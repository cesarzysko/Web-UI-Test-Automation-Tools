namespace Core;

public sealed class DownloadPathGetter
    : IDownloadPathGetter
{
    private string? downloadPath;

    public string GetDownloadPath()
    {
        downloadPath ??= Directory.CreateTempSubdirectory(GetNewDownloadDirectory()).FullName;
        return downloadPath;
    }

    private static string GetNewDownloadDirectory()
    {
        return Guid.NewGuid().ToString();
    }
}