using System.Text.Json.Serialization;

namespace TransformDeveloperTest.Application.Common.Models;
public class StopPointSearchResponse
{
    [JsonPropertyName("query")]
    public string? SearchQuery { get; set; }

    [JsonPropertyName("total")]
    public int TotalCount { get; set; }

    [JsonPropertyName("matches")]
    public MatchedStop[]? MatchedStops { get; set; }
}

