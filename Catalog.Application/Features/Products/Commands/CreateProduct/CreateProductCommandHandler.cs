using AutoMapper;
using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var @product = mapper.Map<Product>(request);
        @product = await productRepository.AddAsync(@product);

        return @product.Id;
    }
}
