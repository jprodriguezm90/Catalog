using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(CatalogDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsProductNameAndBrandUnique(string name, Guid brandId)
    {
        return !await _dbContext.Products.AnyAsync(p => p.Name == name && p.BrandId == brandId);
    }
}
