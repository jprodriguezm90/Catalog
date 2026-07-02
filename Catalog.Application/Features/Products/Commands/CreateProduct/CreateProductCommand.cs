using MediatR;
using System.Text;

namespace Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
    public List<CreateProductStockDto> Stocks { get; set; } = [];

    public override string ToString()
    {
        var strBuilder = new StringBuilder($"Product name: {Name}; Price: {Price};");
        strBuilder.Append($" CategoryId: {CategoryId}; BrandId: {BrandId}; Stocks: [");
        foreach (var item in Stocks)
        {
            strBuilder.Append($"{{ Size: {item.Size}; InStore: {item.InStore}; Online: {item.Online} }}, ");
        }
        return strBuilder.ToString();
    }
}


