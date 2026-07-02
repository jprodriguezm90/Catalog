namespace Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductStockDto
{
    public string Size { get; set; } = string.Empty;
    public int InStore { get; set; }
    public int Online { get; set; }
}
