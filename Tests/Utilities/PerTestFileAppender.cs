using System.Collections.Concurrent;
using log4net.Appender;
using log4net.Core;

namespace Tests;

public sealed class PerTestFileAppender
    : AppenderSkeleton
{
    private const string LogsDirectory = "Logs";

    private readonly ConcurrentDictionary<string, FileAppender> Appenders = new();

    public PerTestFileAppender()
    {
        Directory.CreateDirectory(LogsDirectory);
    }

    public string? GetLogFilePath(string testId)
    {
        return Appenders.TryGetValue(testId, out var appender)
            ? appender.File
            : null;
    }

    protected override void Append(LoggingEvent loggingEvent)
    {
        var testId = loggingEvent.LookupProperty("TestId") as string ?? "Unassigned";
        var appender = Appenders.GetOrAdd(testId, id =>
        {
            var fa = new FileAppender
            {
                Name = $"FileAppender_{id}",
                File = Path.Combine(LogsDirectory, $"{id}.log"),
                AppendToFile = false,
                Layout = Layout
            };
            fa.ActivateOptions();
            return fa;
        });

        appender.DoAppend(loggingEvent);
    }

    protected override void OnClose()
    {
        foreach (var appender in Appenders.Values)
        {
            appender.Close();
        }

        base.OnClose();
    }
}