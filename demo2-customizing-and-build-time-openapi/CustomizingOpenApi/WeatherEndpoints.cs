namespace CustomizingOpenApi;

public static class WeatherEndpoints
{
    public static WebApplication MapWeatherEndpoints(this WebApplication app)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", GetWeatherForecast)
            .WithName("GetWeatherForecast");

        return app;

        WeatherForecast[] GetWeatherForecast()
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
        }
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary, decimal example)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
