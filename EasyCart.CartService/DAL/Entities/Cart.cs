using EasyCart.Shared.DAL.Entities;

namespace EasyCart.CartService.DAL.Entities;

public class Cart : BaseEntity
{
    public required int UserId { get; set; }

    public ICollection<CartItem> Items { get; set; } = [];  
}