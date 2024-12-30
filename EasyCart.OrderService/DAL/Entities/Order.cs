using EasyCart.Shared.DAL.Entities;

namespace EasyCart.OrderService.DAL.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public ICollection<OrderItem> Items { get; set; } = [];
}