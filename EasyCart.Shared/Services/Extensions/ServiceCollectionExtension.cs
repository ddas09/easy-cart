using Microsoft.AspNetCore.Mvc;
using EasyCart.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCart.Shared.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterSharedServices(this IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();

        // Add controllers to the container.
        services.AddControllers();

        // This is to configure custom ModelState validation filter
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // For generating lowecase routes
        services.AddRouting(options => options.LowercaseUrls = true);

        // For registering shared action filters
        services.RegisterSharedActionFilters();

        // For AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // For configuring CORS
        services.AddCors(options =>
        {
            options.AddPolicy("MyCORSPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }
}