using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistration).Assembly));
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ApplicationRegistration).Assembly));

        return services;
    }
}
