using System.Text.Json.Serialization;

namespace Business.Models;

public sealed class Company
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("catchPhrase")]
    public string CatchPhrase { get; init; } = string.Empty;

    [JsonPropertyName("bs")]
    public string Bs { get; init; } = string.Empty;
}