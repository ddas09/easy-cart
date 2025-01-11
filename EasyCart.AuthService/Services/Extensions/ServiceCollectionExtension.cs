using EasyCart.Shared.Services;
using EasyCart.Shared.Services.Contracts;
using EasyCart.AuthService.Services.Contracts;

namespace EasyCart.AuthService.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<ICryptographyService, CryptographyService>();
        services.AddScoped<IAuthService, AuthService>();
        
        services.AddSingleton<IRabbitMQService, RabbitMQService>();
    }
}