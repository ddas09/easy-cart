using EasyCart.Shared.Services;
using EasyCart.Shared.Services.Contracts;

namespace EasyCart.API.Gateway.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
    }
}