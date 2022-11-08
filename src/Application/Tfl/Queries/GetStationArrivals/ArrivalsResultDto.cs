namespace TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;
public class ArrivalsResultDto
{
    public string? StationName { get; set; }

    public IEnumerable<ArrivalsDto>? Arrivals { get; set; }
}
