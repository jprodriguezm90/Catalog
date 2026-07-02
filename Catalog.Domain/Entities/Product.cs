namespace Catalog.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public Guid BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public List<ProductStock> Stocks { get; set; } = [];

}
