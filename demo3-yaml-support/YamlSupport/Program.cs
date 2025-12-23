using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5306");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Map OpenAPI with both JSON and YAML support
app.MapOpenApi();

// Add Scalar UI for viewing the OpenAPI document
app.MapScalarApiReference();

app.MapGet("/weatherforecast", GetWeatherForecast)
    .WithName("GetWeatherForecast")
    .WithSummary("Gets the weather forecast")
    .WithDescription("Returns a collection of weather forecasts for the next 5 days");

app.Run();

static WeatherForecast[] GetWeatherForecast()
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
