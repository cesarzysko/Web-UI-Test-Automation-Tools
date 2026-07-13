using Microsoft.Extensions.Configuration;

namespace Core;

public sealed class JsonFileConfig
    : IConfig
{
    public ConfigData Data { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build()
        .Get<ConfigData>()!;
}