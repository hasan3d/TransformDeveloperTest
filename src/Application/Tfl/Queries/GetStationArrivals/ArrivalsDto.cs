namespace TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;
public class ArrivalsDto
{
    public string? PlatformName { get; set; }
    public string? LineName { get; set; }
    public string? Destination { get; set; }
    public double TimeToStation { get; set; }
    public string? LineColour { get; set; }
}
