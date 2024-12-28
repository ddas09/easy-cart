using System.ComponentModel.DataAnnotations;

namespace EasyCart.ProductService.Models.Request;

public class ProductAddRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Stock must be greater than 0.")]
    public int Stock { get; set; }

    [Required]
    [EmailAddress]
    public string CreatedBy { get; set; }
}