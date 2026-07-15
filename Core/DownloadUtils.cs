namespace Core;

public static class DownloadUtils
{
    public static string GetNewDownloadPath()
    {
        return Directory.CreateTempSubdirectory(GetNewDownloadDirectory()).FullName;
    }

    private static string GetNewDownloadDirectory()
    {
        return Guid.NewGuid().ToString();
    }
}