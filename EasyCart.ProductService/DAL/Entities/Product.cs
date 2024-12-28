using EasyCart.Shared.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace EasyCart.ProductService.DAL.Entities;

public class Product : BaseEntity
{
    [MaxLength(254)]
    public required string Name { get; set; }

    [MaxLength(1000)]
    public required string Description { get; set; }

    public required decimal Price { get; set; }

    public required int Stock { get; set; }  

    public required bool IsActive { get; set; }  
}