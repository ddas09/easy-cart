using EasyCart.Shared.DAL.Repositories;
using EasyCart.CartService.DAL.Entities;
using EasyCart.CartService.DAL.Contracts;

namespace EasyCart.CartService.DAL.Repositories;

public class CartRepository : CrudBaseRepository<Cart, CartDBContext>, ICartRepository
{
    public CartRepository(CartDBContext dbContext) : base(dbContext)
    {
    }
}