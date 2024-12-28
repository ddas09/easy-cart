using System.ComponentModel.DataAnnotations;

namespace EasyCart.CartService.Models.Request;

public class CartItemRemoveRequest
{
    [Required]
    public int CartId { get; set; }

    [Required]
    public int CartItemId { get; set; }
}