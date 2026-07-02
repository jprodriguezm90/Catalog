using AutoMapper;
using Catalog.Application.Features.Products.Commands.CreateProduct;
using Catalog.Application.Features.Products.Queries.GetProductDetail;
using Catalog.Domain.Entities;

namespace Catalog.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, ProductDetailVm>().ReverseMap();

        CreateMap<Category, ProductDetailCategoryDto>().ReverseMap();

        CreateMap<Brand, ProductDetailBrandDto>().ReverseMap();

        CreateMap<ProductStock, ProductDetailStockDto>().ReverseMap();
        CreateMap<ProductStock, CreateProductStockDto>().ReverseMap();


    }
}
