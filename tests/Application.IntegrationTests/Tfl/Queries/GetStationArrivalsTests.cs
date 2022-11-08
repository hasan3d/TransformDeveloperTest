using FluentAssertions;
using NUnit.Framework;
using TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;

namespace TransformDeveloperTest.Application.IntegrationTests.Tfl.Queries;

using static Testing;
public class GetStationArrivalsTests
{

    [Test]
    public async Task ShouldReturnStationNameAndArrivals()
    {
        var query = new GetStationArrivalsQuery();

        var result = await SendAsync(query);

        result.StationName.Should().NotBeNullOrEmpty();

        result.Arrivals.Should().HaveCountGreaterOrEqualTo(1);
    }
}
