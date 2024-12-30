using System.ComponentModel.DataAnnotations;

namespace EasyCart.OrderService.Models.Request;

public class OrderCreateRequest
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public List<OrderItemRequest> Items { get; set; } = []; 
}

public class OrderItemRequest
{
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}
