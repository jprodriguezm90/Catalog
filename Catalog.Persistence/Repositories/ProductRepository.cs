using Catalog.Application.Contracts.Persistence;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories;

public class ProductRepository(CatalogDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    private readonly CatalogDbContext context = context;

    public async Task<bool> IsProductNameAndBrandUnique(string name, Guid brandId)
    {
        return !await context.Products.AnyAsync(p => p.Name == name && p.BrandId == brandId);
    }
}
