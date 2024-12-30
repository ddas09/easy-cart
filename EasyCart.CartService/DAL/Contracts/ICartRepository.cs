using EasyCart.Shared.DAL.Contracts;
using EasyCart.CartService.DAL.Entities;

namespace EasyCart.CartService.DAL.Contracts;

public interface ICartRepository : ICrudBaseRepository<Cart>
{
}