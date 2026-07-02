var builder = WebApplication.CreateBuilder(args);


var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.MapGet("/", () => "Hello World!");

// Map controller endpoints
app.MapControllers();

app.Run();
