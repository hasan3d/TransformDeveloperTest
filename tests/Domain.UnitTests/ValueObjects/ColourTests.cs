using FluentAssertions;
using NUnit.Framework;
using TransformDeveloperTest.Domain.Exceptions;
using TransformDeveloperTest.Domain.ValueObjects;

namespace TransformDeveloperTest.Domain.UnitTests.ValueObjects;

public class ColourTests
{
    [Test]
    public void ShouldReturnCorrectColourCode()
    {
        var code = "#CCFF99";

        var colour = Colour.From(code);

        colour.Code.Should().Be(code);
    }

    [Test]
    public void ToStringReturnsCode()
    {
        var colour = Colour.Green;

        colour.ToString().Should().Be(colour.Code);
    }

    [Test]
    public void ShouldPerformImplicitConversionToColourCodeString()
    {
        string code = Colour.Green;

        code.Should().Be("#CCFF99");
    }

    [Test]
    public void ShouldPerformExplicitConversionGivenSupportedColourCode()
    {
        var colour = (Colour)"#CCFF99";

        colour.Should().Be(Colour.Green);
    }

    [Test]
    public void ShouldThrowUnsupportedColourExceptionGivenNotSupportedColourCode()
    {
        FluentActions.Invoking(() => Colour.From("##FF33CC"))
            .Should().Throw<UnsupportedColourException>();
    }
}
