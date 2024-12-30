using EasyCart.Shared.DAL.Entities;

namespace EasyCart.OrderService.DAL.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}