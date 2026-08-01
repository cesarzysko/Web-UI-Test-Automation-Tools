namespace Core;

public interface IDownloadWaiter
{
    bool IsFileDownloaded(string fileName, TimeSpan timeout);
}