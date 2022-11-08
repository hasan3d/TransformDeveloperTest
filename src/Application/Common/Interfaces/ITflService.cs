using TransformDeveloperTest.Application.Common.Models;
using TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;

namespace TransformDeveloperTest.Application.Common.Interfaces;
public interface ITflService
{
    Task<StopPointSearchResponse> GetStationsAsync(
        CancellationToken cancellationToken);

    Task<ICollection<Arrivals>> GetStationArrivalsAsync(
        string stopPointId,
        CancellationToken cancellationToken);
}
