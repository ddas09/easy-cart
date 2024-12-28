using System.ComponentModel.DataAnnotations;

namespace EasyCart.ProductService.Models.Request;

public class ProductUpdateRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    [EmailAddress]
    public string UpdatedBy { get; set; }
}