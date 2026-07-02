namespace Catalog.Domain.Entities;

public class ProductStock
{
    public Guid Id { get; set; }
    public string Size { get; set; } = string.Empty;
    public int InStore { get; set; }
    public int Online { get; set; }

}
