using Core;
using TechTalk.SpecFlow;

namespace Tests;

[Binding]
public class FileDownloadSteps
{
    private static readonly TimeSpan DownloadTimeout = TimeSpan.FromSeconds(5);

    private readonly IDownloadWaiter DownloadWaiter;

    public FileDownloadSteps(IDownloadWaiter downloadWaiter)
    {
        DownloadWaiter = downloadWaiter;
    }

    [Then("the file (.*) should be downloaded")]
    public void ThenTheFileShouldBeDownloaded(string fileName)
    {
        var downloadedFile = DownloadWaiter.GetDownloadedFileName(DownloadTimeout);
        Assert.That(downloadedFile, Is.EqualTo(fileName));
    }
}