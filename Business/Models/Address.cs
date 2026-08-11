using System.Text.Json.Serialization;

namespace Business.Models;

public sealed class Address
{
    [JsonPropertyName("street")]
    public string Street { get; init; } = string.Empty;

    [JsonPropertyName("suite")]
    public string Suite { get; init; } = string.Empty;

    [JsonPropertyName("city")]
    public string City { get; init; } = string.Empty;

    [JsonPropertyName("zipcode")]
    public string Zipcode { get; init; } = string.Empty;

    [JsonPropertyName("geo")]
    public Geo Geo { get; init; } = new();
}