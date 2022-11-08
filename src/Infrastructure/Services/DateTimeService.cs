using TransformDeveloperTest.Application.Common.Interfaces;

namespace TransformDeveloperTest.Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
