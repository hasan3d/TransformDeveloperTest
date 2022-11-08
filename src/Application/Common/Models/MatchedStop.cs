using System.Text.Json.Serialization;

namespace TransformDeveloperTest.Application.Common.Models;
public class MatchedStop
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
