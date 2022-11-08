using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WebUI.Filters;

namespace WebUI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddControllersWithViews(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TransformDeveloperTest API", Version = "v1" });
        });

        return services;
    }
}
