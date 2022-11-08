using Refit;
using TransformDeveloperTest.Application.Common.Models;
using TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;

namespace TransformDeveloperTest.Application.Common.Interfaces;
public interface ITflClient
{
    [Get("/StopPoint/Search?query={stationName}")]
    Task<ApiResponse<StopPointSearchResponse>> GetStationsAsync(
        string stationName,
        CancellationToken cancellationToken);

    [Get("/StopPoint/{stopPointId}/Arrivals")]
    Task<ApiResponse<ICollection<Arrivals>>> GetStationArrivalsAsync(
        string stopPointId,
        CancellationToken cancellationToken);
}
