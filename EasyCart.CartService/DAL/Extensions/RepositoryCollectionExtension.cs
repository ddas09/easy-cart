using EasyCart.CartService.DAL.Contracts;
using EasyCart.CartService.DAL.Repositories;

namespace EasyCart.CartService.DAL.Extensions;

public static class RepositoryCollection
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICartRepository, CartRepository>();

        services.AddScoped<ICartItemRepository, CartItemRepository>();
    }
}
