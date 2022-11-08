using Microsoft.Extensions.Caching.Memory;
using Refit;
using TransformDeveloperTest.Application.Common.Interfaces;
using TransformDeveloperTest.Application.Common.Models;
using TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;

namespace TransformDeveloperTest.Infrastructure.Services;
public class TflService : ITflService
{
    private readonly ITflClient _client;

    private readonly IMemoryCache _cache;

    private readonly TflOptions _options;

    public TflService(ITflClient client, IMemoryCache cache, TflOptions options)
    {
        _client = client;
        _cache = cache;
        _options = options;
    }

    public Task<StopPointSearchResponse> GetStationsAsync(CancellationToken cancellationToken)
    {
        var stationName = _options.StationName ?? "Great Portland Street Underground Station";
        
        var cacheKey = $"TfL.Station.{stationName}.SearchResponse";

        return GetResponseWithCachingAsync(
            cacheKey,
            () => _client.GetStationsAsync(stationName, cancellationToken));
    }

    public Task<ICollection<Arrivals>> GetStationArrivalsAsync(string stopPointId, CancellationToken cancellationToken)
    {
        var cacheKey = $"TfL.{stopPointId}.Arrivals";

        return GetResponseWithCachingAsync(
            cacheKey,
            () => _client.GetStationArrivalsAsync(stopPointId, cancellationToken));
    }

    private async Task<T> GetResponseWithCachingAsync<T>(string cacheKey, Func<Task<ApiResponse<T>>> operation)
    {
        if (!_cache.TryGetValue(cacheKey, out T? result))
        {
            using var response = await operation();
            await response.EnsureSuccessStatusCodeAsync();

            result = response.Content;

            if (!string.IsNullOrEmpty(cacheKey) &&
                response.Headers.CacheControl != null &&
                response.Headers.CacheControl.MaxAge.HasValue)
            {
                _cache.Set(cacheKey, result, response.Headers.CacheControl.MaxAge.Value);
            }
        }

        return result!;
    }
}
