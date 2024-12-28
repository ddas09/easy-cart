using System.ComponentModel.DataAnnotations;

namespace EasyCart.CartService.Models.Request;

public class CartItemUpdateRequest
{
    [Required]
    public int CartId { get; set; }

    [Required]
    public int CartItemId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal Price { get; set; }
}