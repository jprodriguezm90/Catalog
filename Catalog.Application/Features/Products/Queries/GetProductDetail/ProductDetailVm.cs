using Catalog.Application.Features.Products.Commands.CreateProduct;

namespace Catalog.Application.Features.Products.Queries.GetProductDetail;

public class ProductDetailVm
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public List<ProductDetailStockDto> Stocks { get; set; } = [];
}
