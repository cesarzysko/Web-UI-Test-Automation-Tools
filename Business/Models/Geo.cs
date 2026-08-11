using System.Text.Json.Serialization;

namespace Business.Models;

public sealed class Geo
{
    [JsonPropertyName("lat")]
    public string Lat { get; init; } = string.Empty;

    [JsonPropertyName("lng")]
    public string Lng { get; init; } = string.Empty;
}