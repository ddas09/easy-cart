using EasyCart.OrderService.DAL.Contracts;
using EasyCart.OrderService.DAL.Repositories;

namespace EasyCart.OrderService.DAL.Extensions;

public static class RepositoryCollection
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
    }
}
