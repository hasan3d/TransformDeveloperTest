using System.Text.Json.Serialization;

namespace TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;
public class Arrivals
{
    [JsonPropertyName("platformName")]
    public string? PlatformName { get; set; }

    [JsonPropertyName("stationName")]
    public string? StationName { get; set; }

    [JsonPropertyName("lineId")]
    public string? LineId { get; set; }

    [JsonPropertyName("lineName")]
    public string? LineName { get; set; }

    [JsonPropertyName("towards")]
    public string? Destination { get; set; }

    [JsonPropertyName("expectedArrival")]
    public DateTime? ExpectedArrival { get; set; }
}
