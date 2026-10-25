using Microsoft.OpenApi;

namespace CustomizingOpenApi;

public static class WeatherEndpoints
{
    public static WebApplication MapWeatherEndpoints(this WebApplication app)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)],
                    42
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithTags("Forecasts");

        app.MapGet("/old-endpoint", [Obsolete("Use /weatherforecast")] () => "This is an old endpoint")
        .WithName("old-endpoint")
        .WithTags("Forecasts");

        app.MapGet("/weather-alerts", () => new[] { new WeatherAlert("Storm", "Strong winds along the coast of Malmö") })
        .WithName("GetWeatherAlerts")
        .WithTags("Alerts")
        .AddOpenApiOperationTransformer((operation, context, cancellationToken) =>
        {
            operation.ExternalDocs = new OpenApiExternalDocs
            {
                Description = "Official weather warnings for Sweden",
                Url = new Uri("https://www.smhi.se/en")
            };
            return Task.CompletedTask;
        });

        return app;
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary, decimal example)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal record WeatherAlert(string Title, string Description);
