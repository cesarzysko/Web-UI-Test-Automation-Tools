namespace Core;

public interface IDownloadWaiter
{
    string GetDownloadedFileName(TimeSpan timeout);
}