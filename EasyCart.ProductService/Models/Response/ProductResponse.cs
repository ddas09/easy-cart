namespace EasyCart.ProductService.Models.Response;

public class ProductResponse
{
    public List<ProductInformation> Products { get; set; } = [];
}

public class ProductInformation
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string ImageURL { get; set; } = string.Empty;
}