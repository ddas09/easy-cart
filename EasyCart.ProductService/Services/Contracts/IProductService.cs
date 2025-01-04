using EasyCart.ProductService.Models.Request;
using EasyCart.ProductService.Models.Response;

namespace EasyCart.ProductService.Services.Contracts;

public interface IProductService
{
    Task<ProductResponse> GetAllProducts();

    Task<ProductInformation> GetProduct(int productId);

    Task<ProductInformation> AddProduct(ProductAddRequest request);

    Task<ProductInformation> UpdateProduct(ProductUpdateRequest request);

    Task DeleteProduct(ProductDeleteRequest request);
}