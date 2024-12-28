using System.ComponentModel.DataAnnotations;

namespace EasyCart.ProductService.Models.Request;

public class ProductDeleteRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string DeletedBy { get; set; }
}