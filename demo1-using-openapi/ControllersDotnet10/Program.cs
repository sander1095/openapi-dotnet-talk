using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5301");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(
    // TODO: Remove
    x => x.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference(); // As a replacement for SwaggerUI!

app.UseAuthorization();

app.MapControllers();

app.Run();
