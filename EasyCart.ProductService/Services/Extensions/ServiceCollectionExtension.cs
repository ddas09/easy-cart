using EasyCart.ProductService.Services.Contracts;

namespace EasyCart.ProductService.Services.Extensions;

public static class ServiceCollection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
    }
}