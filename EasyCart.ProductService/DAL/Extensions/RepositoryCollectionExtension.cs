using EasyCart.ProductService.DAL.Contracts;
using EasyCart.ProductService.DAL.Repositories;

namespace EasyCart.ProductService.DAL.Extensions;

public static class RepositoryCollection
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
