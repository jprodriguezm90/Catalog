using Catalog.Application.Contracts.Persistence;
using Catalog.Infrastructure.Implementation.Persistence;
using Catalog.Infrastructure.Implementation.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Implementation;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CatalogDatabase")
            ?? throw new InvalidOperationException("Connection string 'CatalogDatabase' was not found.");

        services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
