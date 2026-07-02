using Catalog.Domain.Entities;

namespace Catalog.Application.Contracts.Persistence;

public interface IProductRepository : IAsyncRepository<Product>
{
    Task<bool> IsProductNameAndBrandUnique(string name, Guid brandId);
    Task<Product?> GetProductDetailAsync(Guid id);
}
