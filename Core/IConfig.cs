using Microsoft.Extensions.Logging;

namespace Core;

public interface IConfig
{
    ConfigData Data { get; }
}