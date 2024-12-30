namespace EasyCart.OrderService.Models.Response;

public class OrderResponse
{
    public int OrderId { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public List<OrderItemInformation> Items { get; set; } = [];
}

public class OrderItemInformation
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}
