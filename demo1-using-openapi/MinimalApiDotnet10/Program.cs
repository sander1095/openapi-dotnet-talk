using MinimalApiDotnet10;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5302");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(x =>
    // TODO: Remove
    x.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0
);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference(); // As a replacement for SwaggerUI!

app.MapTalkEndpoints();

app.Run();