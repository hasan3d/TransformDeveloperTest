using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransformDeveloperTest.Application.Common.Behaviours;
using TransformDeveloperTest.Application.Common.Interfaces;
using TransformDeveloperTest.Application.Tfl;

namespace TransformDeveloperTest.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IExpectedArrivalDateTime, ExpectedArrivalDateTime>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        return services;
    }
}
