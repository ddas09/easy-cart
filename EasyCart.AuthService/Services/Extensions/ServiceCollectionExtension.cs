using EasyCart.AuthService.Services.Contracts;

namespace EasyCart.AuthService.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICryptographyService, CryptographyService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}