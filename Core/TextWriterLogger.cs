using Microsoft.Extensions.Logging;

namespace Core;

public class TextWriterLogger
    : ILogger
{
    private readonly TextWriter textWriter;
    private readonly LogLevel minLevel;

    public TextWriterLogger(TextWriter textWriter, LogLevel minLevel = LogLevel.Information)
    {
        this.textWriter = textWriter;
        this.minLevel = minLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        textWriter.WriteLine($"[{logLevel}] {formatter.Invoke(state, exception)}");
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= minLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}