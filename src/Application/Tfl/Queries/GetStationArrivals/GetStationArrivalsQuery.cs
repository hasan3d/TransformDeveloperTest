using MediatR;
using TransformDeveloperTest.Application.Common.Interfaces;
using TransformDeveloperTest.Application.Tfl.Extensions;

namespace TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;
public record GetStationArrivalsQuery : IRequest<ArrivalsResultDto>;

public class GetStationArrivalsQueryHandler : IRequestHandler<GetStationArrivalsQuery, ArrivalsResultDto>
{
    private readonly ITflService _tflService;
    private readonly IExpectedArrivalDateTime _expectedArrivalDateTime;

    public GetStationArrivalsQueryHandler(ITflService tflService, IExpectedArrivalDateTime expectedArrivalDateTime)
    {
        _tflService = tflService;
        _expectedArrivalDateTime = expectedArrivalDateTime;
    }
    public async Task<ArrivalsResultDto> Handle(GetStationArrivalsQuery request, CancellationToken cancellationToken)
    {
        var station = await _tflService.GetStationsAsync(cancellationToken).ConfigureAwait(false);

        if (station.TotalCount >= 1 && station.MatchedStops != null && station.MatchedStops.Any())
        {
            var stopPointId = station.MatchedStops.FirstOrDefault()?.Id;

            if (!string.IsNullOrEmpty(stopPointId))
            {
                var stationArrivals = await _tflService.GetStationArrivalsAsync(stopPointId, cancellationToken).ConfigureAwait(false);

                var arrivalsResult = stationArrivals.Select(x => new ArrivalsDto()
                {
                    PlatformName = x.PlatformName,
                    Destination = x.Destination,
                    LineName = x.LineName,
                    LineColour = x.LineId?.MapLineColour(),
                    TimeToStation = _expectedArrivalDateTime.GetExpectedArrivalTimeInMinutes(x.ExpectedArrival),
                }).OrderBy(x => x.TimeToStation);

                return new ArrivalsResultDto()
                {
                    StationName = station.MatchedStops.FirstOrDefault()?.Name,
                    Arrivals = arrivalsResult
                };
            }
        }

        return new ArrivalsResultDto();

    }
}