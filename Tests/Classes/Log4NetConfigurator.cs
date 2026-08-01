using Core;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;

namespace Tests;

public sealed class Log4NetConfigurator
    : ILog4NetConfigurator
{
    private readonly LoggingSettings Settings;

    public Log4NetConfigurator(LoggingSettings loggingSettings)
    {
        Settings = loggingSettings;
    }

    void ILog4NetConfigurator.Configure()
    {
        var hierarchy = (Hierarchy)LogManager.GetRepository();
        hierarchy.ResetConfiguration();
        XmlConfigurator.Configure(hierarchy, new FileInfo("Config/log4net.config"));
        hierarchy.Root.Level = Settings.LogLevel.ToLog4NetLevel();
        hierarchy.HandleConsoleOutput(Settings.ConsoleOutput);
        hierarchy.HandleFileOutput(Settings.FileOutput);
        hierarchy.Configured = true;
    }
}