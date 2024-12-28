using EasyCart.Shared.DAL.Contracts;
using EasyCart.ProductService.DAL.Entities;

namespace EasyCart.ProductService.DAL.Contracts;

public interface IProductRepository : ICrudBaseRepository<Product>
{
}