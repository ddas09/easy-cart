using EasyCart.Shared.DAL.Entities;

namespace EasyCart.CartService.DAL.Entities;

public class CartItem : BaseEntity
{
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
    
    public decimal Price { get; set; } 
}