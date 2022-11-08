using System.Text.Json;
using FluentAssertions;
using JustEat.HttpClientInterception;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using Refit;
using TransformDeveloperTest.Application.Common.Interfaces;
using TransformDeveloperTest.Application.Common.Models;
using TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;
using TransformDeveloperTest.Infrastructure.Services;

namespace TransformDeveloperTest.Infrastructure.IntegrationTests.Services;

public class TflServiceTests : IDisposable
{
    private IMemoryCache _cache;
    private TflOptions _options;
    private HttpClientInterceptorOptions _interceptor;

    [SetUp]
    public void SetUp()
    {
        _interceptor = new HttpClientInterceptorOptions()
        {
            ThrowOnMissingRegistration = true,
        };

        _options = CreateOptions();
        _cache = CreateCache();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        _cache?.Dispose();
    }

    [Test]
    public async Task ShouldReturnStationInformationWithResponseCached()
    {
        // Arrange

        var expected = new { query = "great portland street", total = 1, matches = new[] {new {id = "940GZZLUGPS", name = "Great Portland Street Underground Station" } } };

        var builder = CreateBuilder()
            .Requests()
            .ForPath("StopPoint/Search")
            .ForQuery("query=Great Portland Street Underground Station")
            .Responds()
            .WithResponseHeader("Cache-Control", "max-age=302400")
            .WithJsonContent(expected);

        _interceptor.Register(builder);

        using var httpClient = _interceptor.CreateHttpClient();
        httpClient.BaseAddress = _options.BaseUri;

        ITflClient client = CreateClient(httpClient);
        var target = new TflService(client, _cache, _options);

        // Act
        StopPointSearchResponse actual1 = await target.GetStationsAsync(default);
        StopPointSearchResponse actual2 = await target.GetStationsAsync(default);

        // Assert

        actual1.Should().NotBeNull();
        actual1.TotalCount.Should().Be(1);
        actual1.MatchedStops.Should().NotBeNull();
        actual1.MatchedStops.Should().HaveCount(1);
        actual1.MatchedStops?.FirstOrDefault()?.Id.Should().Be("940GZZLUGPS");
        actual1.MatchedStops?.FirstOrDefault()?.Name.Should().Be("Great Portland Street Underground Station");
        actual1.SearchQuery.Should().Be("great portland street");

        actual1.Should().Be(actual2);
    }

    [Test]
    public async Task ShouldReturnStationInformationWithResponseNotCached()
    {
        // Arrange

        var expected = new { query = "great portland street", total = 1, matches = new[] { new { id = "940GZZLUGPS", name = "Great Portland Street Underground Station" } } };

        var builder = CreateBuilder()
            .Requests()
            .ForPath("StopPoint/Search")
            .ForQuery("query=Great Portland Street Underground Station")
            .Responds()
            .WithJsonContent(expected);

        _interceptor.Register(builder);

        using var httpClient = _interceptor.CreateHttpClient();
        httpClient.BaseAddress = _options.BaseUri;

        ITflClient client = CreateClient(httpClient);
        var target = new TflService(client, _cache, _options);

        // Act
        StopPointSearchResponse actual1 = await target.GetStationsAsync(default);
        StopPointSearchResponse actual2 = await target.GetStationsAsync(default);

        // Assert

        actual1.Should().NotBeNull();
        actual1.TotalCount.Should().Be(1);
        actual1.MatchedStops.Should().NotBeNull();
        actual1.MatchedStops.Should().HaveCount(1);
        actual1.MatchedStops?.FirstOrDefault()?.Id.Should().Be("940GZZLUGPS");
        actual1.MatchedStops?.FirstOrDefault()?.Name.Should().Be("Great Portland Street Underground Station");
        actual1.SearchQuery.Should().Be("great portland street");

        actual1.Should().NotBe(actual2);
    }

    [Test]
    public async Task ShouldReturnArrivalsInformationWithResponseCached()
    {
        // Arrange

        var stopPointId = "940GZZLUGPS";

        var stationName = "Great Portland Street Underground Station";

        var expected = new[] { new { stationName = stationName, towards = "Edgware Road (Circle)",
            lineId = "circle", lineName = "Circle", platformName = "Eastbound - Platform 2", expectedArrival = "2022-11-07T18:22:27Z" } };

        var builder = CreateBuilder()
            .Requests()
            .ForPath("StopPoint/940GZZLUGPS/Arrivals")
            .Responds()
            .WithResponseHeader("Cache-Control", "max-age=30")
            .WithJsonContent(expected);

        _interceptor.Register(builder);

        using var httpClient = _interceptor.CreateHttpClient();
        httpClient.BaseAddress = _options.BaseUri;

        ITflClient client = CreateClient(httpClient);
        var target = new TflService(client, _cache, _options);

        // Act
        ICollection<Arrivals> actual1 = await target.GetStationArrivalsAsync(stopPointId, default);
        ICollection<Arrivals> actual2 = await target.GetStationArrivalsAsync(stopPointId,default);

        // Assert

        var item = actual1.FirstOrDefault();

        item.Should().NotBeNull();
        item?.StationName.Should().Be(expected.First().stationName);
        item?.PlatformName.Should().Be(expected.First().platformName);
        item?.Destination.Should().Be(expected.First().towards);
        item?.LineId.Should().Be(expected.First().lineId);
        item?.LineName.Should().Be(expected.First().lineName);
        item?.ExpectedArrival.Should().Be(DateTime.Parse(expected.First().expectedArrival));

        actual1.Should().BeSameAs(actual2);
    }

    [Test]
    public async Task ShouldReturnArrivalsInformationWithResponseNotCached()
    {
        // Arrange

        var stopPointId = "940GZZLUGPS";

        var stationName = "Great Portland Street Underground Station";

        var expected = new[] { new { stationName = stationName, towards = "Edgware Road (Circle)",
            lineId = "circle", lineName = "Circle", platformName = "Eastbound - Platform 2", expectedArrival = "2022-11-07T18:22:27Z" } };

        var builder = CreateBuilder()
            .Requests()
            .ForPath("StopPoint/940GZZLUGPS/Arrivals")
            .Responds()
            .WithJsonContent(expected);

        _interceptor.Register(builder);

        using var httpClient = _interceptor.CreateHttpClient();
        httpClient.BaseAddress = _options.BaseUri;

        ITflClient client = CreateClient(httpClient);
        var target = new TflService(client, _cache, _options);

        // Act
        ICollection<Arrivals> actual1 = await target.GetStationArrivalsAsync(stopPointId, default);
        ICollection<Arrivals> actual2 = await target.GetStationArrivalsAsync(stopPointId, default);

        // Assert

        var item = actual1.FirstOrDefault();

        item.Should().NotBeNull();
        item?.StationName.Should().Be(expected.First().stationName);
        item?.PlatformName.Should().Be(expected.First().platformName);
        item?.Destination.Should().Be(expected.First().towards);
        item?.LineId.Should().Be(expected.First().lineId);
        item?.LineName.Should().Be(expected.First().lineName);
        item?.ExpectedArrival.Should().Be(DateTime.Parse(expected.First().expectedArrival));

        actual1.Should().NotBeSameAs(actual2);
    }


    private static HttpRequestInterceptionBuilder CreateBuilder()
    {
        return new HttpRequestInterceptionBuilder()
            .ForHttps()
            .ForHost("api.tfl.gov.uk");
    }

    private static IMemoryCache CreateCache()
    {
        var cacheOptions = new MemoryCacheOptions();
        var options = Microsoft.Extensions.Options.Options.Create(cacheOptions);

        return new MemoryCache(options);
    }

    private static ITflClient CreateClient(HttpClient httpClient)
    {
        var settings = new RefitSettings() { ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions()) };

        return RestService.For<ITflClient>(httpClient, settings);
    }

    private static TflOptions CreateOptions()
    {
        return new TflOptions()
        {
            BaseUri = new Uri("https://api.tfl.gov.uk/"),
            StationName = "Great Portland Street Underground Station"
        };
    }
}