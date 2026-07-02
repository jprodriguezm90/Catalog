using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductDetail;

public class GetProductDetailQuery : IRequest<ProductDetailVm>
{
    public Guid ProductId { get; set; }
}
