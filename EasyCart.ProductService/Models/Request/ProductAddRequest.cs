using System.ComponentModel.DataAnnotations;

namespace EasyCart.ProductService.Models.Request;

public class ProductAddRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, 100000, ErrorMessage = "Price must be in the range of 100 to 1 lakh.")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "Stock must be greater than 1 to 1000.")]
    public int Stock { get; set; }

    [Required]
    public string ImageUrl{ get; set; }

    [Required]
    [EmailAddress]
    public string CreatedBy { get; set; }
}