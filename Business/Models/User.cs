using System.Text.Json.Serialization;

namespace Business.Models;

public sealed class User
{
    [JsonPropertyName("id")]
    public int Id { get; init; } = 0;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("username")]
    public string Username { get; init; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("address")]
    public Address Address { get; init; } = new();

    [JsonPropertyName("phone")]
    public string Phone { get; init; } = string.Empty;

    [JsonPropertyName("website")]
    public string Website { get; init; } = string.Empty;

    [JsonPropertyName("company")]
    public Company Company { get; init; } = new();
}