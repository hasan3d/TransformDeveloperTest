using FluentAssertions;
using Moq;
using NUnit.Framework;
using TransformDeveloperTest.Application.Common.Interfaces;
using TransformDeveloperTest.Application.Tfl;

namespace TransformDeveloperTest.Application.UnitTests.Tfl;
public class ExpectedArrivalDateTimeTests
{
    private Mock<IDateTime> _dateTimeService;
    private ExpectedArrivalDateTime _sut;

    [SetUp]
    public void Setup()
    {
        _dateTimeService = new Mock<IDateTime>();

        _sut = new ExpectedArrivalDateTime(_dateTimeService.Object);

    }

    [Theory]
    [TestCase("2022-11-07T21:57:08Z", "2022-11-07T21:56:08Z", 1)]
    [TestCase("2022-11-07T21:58:08Z", "2022-11-07T21:56:08Z", 2)]
    [TestCase("2022-11-07T21:59:08Z", "2022-11-07T21:56:08Z", 3)]
    public void ShouldGetExpectedArrivalTimeInMinutes(string inputArrivalsDateTime, string currentDataTime, double expectedTotalMinutes)
    {
        // Arrange
        _dateTimeService.Setup(x => x.Now).Returns(DateTime.Parse(currentDataTime));

        // Act

        var resultInMinute = _sut.GetExpectedArrivalTimeInMinutes(DateTime.Parse(inputArrivalsDateTime));

        // Assert
        resultInMinute.Should().Be(expectedTotalMinutes);

        _dateTimeService.Verify(x => x.Now, Times.Once);
    }
}
