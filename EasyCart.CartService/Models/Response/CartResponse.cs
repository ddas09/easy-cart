namespace EasyCart.CartService.Models.Response;

public class CartResponse
{
    public int Id { get; set; }

    public List<CartItemInformation> CartItems { get; set; } = [];
}

public class CartItemInformation
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ProductImageURL { get; set; } = string.Empty;

    public string ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}