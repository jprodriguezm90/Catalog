using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductDetail;

public class GetProductDetailHandler(IMapper mapper, IAsyncRepository<Product> productRepository) : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
{
    public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var @product = await productRepository.GetByIdAsync(request.ProductId);
        
        var productDetailVm = mapper.Map<ProductDetailVm>(@product);

        return productDetailVm;
    }
}