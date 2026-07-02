using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Exceptions;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductDetail;

public class GetProductDetailHandler(IMapper mapper, IProductRepository productRepository) : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
{
    public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        
        var product = await productRepository.GetProductDetailAsync(request.ProductId);

        if (product is null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        var productDetailVm = mapper.Map<ProductDetailVm>(product);
        productDetailVm.BrandName = product.Brand.Name;
        productDetailVm.CategoryName = product.Category.Name;

        return productDetailVm;
    }
}