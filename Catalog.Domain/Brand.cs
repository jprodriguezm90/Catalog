namespace Catalog.Domain;

public class Brand
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

}
