namespace TransformDeveloperTest.Application.Common.Interfaces;
public interface IExpectedArrivalDateTime
{
    double GetExpectedArrivalTimeInMinutes(DateTime? inputDateTime);
}
