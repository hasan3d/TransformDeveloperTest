using FluentAssertions;
using NUnit.Framework;
using TransformDeveloperTest.Application.Tfl.Extensions;

namespace TransformDeveloperTest.Application.UnitTests.Tfl.Extensions;
public class LineColourMapExtensionsTests
{

    [Theory]
    [TestCase("hammersmith-city", "#FFC0CB")]
    [TestCase("circle", "#FFFF66")]
    [TestCase("metropolitan", "#9B0056")]
    public void ShouldMapLineColour(string lineId, string expected)
    {
        var result = lineId.MapLineColour();

        result.Should().Be(expected);
    }

}
