using EasyCart.Shared.DAL.Repositories;
using EasyCart.CartService.DAL.Entities;
using EasyCart.CartService.DAL.Contracts;

namespace EasyCart.CartService.DAL.Repositories;

public class CartItemRepository : CrudBaseRepository<CartItem, CartDBContext>, ICartItemRepository
{
    public CartItemRepository(CartDBContext dbContext) : base(dbContext)
    {
    }
}