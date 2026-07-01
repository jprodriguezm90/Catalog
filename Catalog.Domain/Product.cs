namespace Catalog.Domain;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int Size { get; set; }
    public List<Category> Categories { get; set; } = [];
    public Brand Brand { get; set; } = null!;
    public Stock Stock { get; set; } = new();

}
