using EasyCart.Shared.DAL.Repositories;
using EasyCart.ProductService.DAL.Entities;
using EasyCart.ProductService.DAL.Contracts;

namespace EasyCart.ProductService.DAL.Repositories;

public class ProductRepository : CrudBaseRepository<Product, ProductDBContext>, IProductRepository
{
    public ProductRepository(ProductDBContext dbContext) : base(dbContext)
    {
    }
}