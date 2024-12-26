using EasyCart.Shared.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCart.Shared.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterSharedServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
    }
}