namespace MyCom.Domain.Products;

public class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? BlurHash { get; set; }

    public string? ImageUrl { get; set; }

    public decimal ListPrice { get; set; }
}
