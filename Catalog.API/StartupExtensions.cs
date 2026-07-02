using Catalog.Application;
using Catalog.Persistence;

public static class StartupExtensions
{ 
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplication();

        builder.Services.AddPersistence(builder.Configuration);

        builder.Services.AddHttpContextAccessor();

        // Register MVC controllers so attribute routed controllers (like ProductsController) are discovered
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}