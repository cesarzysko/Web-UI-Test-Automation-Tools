namespace Core;

public sealed class LoggingSettings
{
    public LogLevel LogLevel { get; init; }
    public bool ConsoleOutput { get; init; }
    public bool FileOutput { get; init; }
}