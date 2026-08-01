using Core;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;

namespace Tests;

public sealed class Log4NetConfigurator
    : ILog4NetConfigurator
{
    private readonly LoggingSettings settings;

    public Log4NetConfigurator(LoggingSettings loggingSettings)
    {
        settings = loggingSettings;
    }

    void ILog4NetConfigurator.Configure()
    {
        var hierarchy = (Hierarchy)LogManager.GetRepository();
        hierarchy.ResetConfiguration();
        XmlConfigurator.Configure(hierarchy, new FileInfo("Config/log4net.config"));
        hierarchy.Root.Level = settings.LogLevel.ToLog4NetLevel();
        hierarchy.HandleConsoleOutput(settings.ConsoleOutput);
        hierarchy.HandleFileOutput(settings.FileOutput);
        hierarchy.Configured = true;
    }
}