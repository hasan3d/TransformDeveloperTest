using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using TransformDeveloperTest.Application.Common.Interfaces;
using TransformDeveloperTest.Infrastructure.Services;
using Microsoft.Extensions.Options;
using Refit;
using TransformDeveloperTest.Application.Common.Models;

namespace TransformDeveloperTest.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<ITflService, TflService>();
        services.AddTransient((p) => p.GetRequiredService<IOptionsMonitor<TflOptions>>().CurrentValue!);

        services.AddSingleton<IHttpContentSerializer>((p) => new SystemTextJsonContentSerializer(new JsonSerializerOptions()));

        services
            .AddHttpClient(nameof(ITflClient))
            .AddTypedClient(AddTfl);

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }

    private static ITflClient AddTfl(HttpClient client, IServiceProvider provider)
    {
        client.BaseAddress = provider.GetRequiredService<TflOptions>().BaseUri;

        var settings = new RefitSettings()
        {
            ContentSerializer = provider.GetRequiredService<IHttpContentSerializer>(),
            HttpMessageHandlerFactory = () => provider.GetRequiredService<IHttpMessageHandlerFactory>().CreateHandler(),
        };

        return RestService.For<ITflClient>(client, settings);
    }
}
