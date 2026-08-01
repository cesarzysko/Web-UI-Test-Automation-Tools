using log4net.Core;

namespace Core;

public static class Log4NetUtils
{
    public static Level ToLog4NetLevel(this LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Info => Level.Info,
            LogLevel.Warn => Level.Warn,
            LogLevel.Error => Level.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };
    }
}