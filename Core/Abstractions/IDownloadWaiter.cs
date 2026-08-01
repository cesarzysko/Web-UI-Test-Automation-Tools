namespace Core;

public interface IDownloadWaiter
{
    string GetDownloadedFile(TimeSpan timeout);
}