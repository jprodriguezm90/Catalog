using Catalog.Application.Features.Products.Commands.CreateProduct;

namespace Catalog.Application.Features.Products.Queries.GetProductDetail;

public class ProductDetailVm
{
    public required string Name { get; set; }
    public double Price { get; set; }
    public Guid CategoryId { get; set; }
    public ProductDetailCategoryDto? Category { get; set; }
    public Guid BrandId { get; set; }
    public ProductDetailBrandDto? Brand { get; set; }
    public List<ProductDetailStockDto> Stocks { get; set; } = [];
}
