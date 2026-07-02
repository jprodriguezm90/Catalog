namespace Catalog.Domain;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public Guid BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public List<Stock> Stocks { get; set; } = [];

}
