using Microsoft.Extensions.Configuration;

namespace Core;

public sealed class ConfigurationFileConfig
    : IConfig
{
    private static readonly string ConfigPath = Path.Combine("Config", "appsettings.json");

    public ConfigData Data { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile(ConfigPath, optional: false, reloadOnChange: false)
        .Build()
        .Get<ConfigData>()!;
}