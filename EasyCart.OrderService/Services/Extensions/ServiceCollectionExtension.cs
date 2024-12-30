using EasyCart.OrderService.Services.Contracts;

namespace EasyCart.OrderService.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
    }
}