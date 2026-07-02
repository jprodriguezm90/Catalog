using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProductDetail;

public class GetProductDetailHandler(IMapper mapper, IAsyncRepository<Product> productRepository, IAsyncRepository<Category> categoryRepository, IAsyncRepository<Brand> brandRepository) : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
{
    public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        var brand = await brandRepository.GetByIdAsync(product.BrandId);
        var category = await categoryRepository.GetByIdAsync(product.CategoryId);

        var productDetailVm = mapper.Map<ProductDetailVm>(product);
        productDetailVm.BrandName = mapper.Map<ProductDetailBrandDto>(brand).Name;
        productDetailVm.CategoryName = mapper.Map<ProductDetailCategoryDto>(category).Name;

        return productDetailVm;
    }
}