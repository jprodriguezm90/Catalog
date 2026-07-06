using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.IntegrationTests.Context;

public class CatalogDbContextTests
{
    public readonly CatalogDbContext _catalogDbContext;
    public CatalogDbContextTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<CatalogDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _catalogDbContext = new CatalogDbContext(dbContextOptions);
    }
}
