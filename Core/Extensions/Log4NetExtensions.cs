using log4net.Core;

namespace Core;

public static class Log4NetExtensions
{
    extension(LogLevel logLevel)
    {
        public Level ToLog4NetLevel()
        {
            return logLevel switch
            {
                LogLevel.Debug => Level.Debug,
                LogLevel.Info => Level.Info,
                LogLevel.Warn => Level.Warn,
                LogLevel.Error => Level.Error,
                _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
            };
        }
    }
}