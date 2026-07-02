namespace Catalog.Application.Features.Products.Queries.GetProductDetail;

public class ProductDetailStockDto
{
    public string Size { get; set; } = string.Empty;
    public int InStore { get; set; }
    public int Online { get; set; }
}