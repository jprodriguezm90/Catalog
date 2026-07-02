using Catalog.Application;
using Catalog.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
// Register MVC controllers so attribute routed controllers (like ProductsController) are discovered
builder.Services.AddControllers();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Map controller endpoints
app.MapControllers();

app.Run();
