using TransformDeveloperTest.Application.Common.Interfaces;

namespace TransformDeveloperTest.Application.Tfl;
public class ExpectedArrivalDateTime : IExpectedArrivalDateTime
{
    private readonly IDateTime _dateTimeService;
    public ExpectedArrivalDateTime(IDateTime dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }
    public double GetExpectedArrivalTimeInMinutes(DateTime? inputDateTime)
    {
        var utcNow = _dateTimeService.Now;

        return inputDateTime.GetValueOrDefault().Subtract(utcNow).TotalMinutes;
    }
}
