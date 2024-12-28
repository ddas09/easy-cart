using EasyCart.CartService.Services.Contracts;

namespace EasyCart.CartService.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICartService, CartService>();
    }
}