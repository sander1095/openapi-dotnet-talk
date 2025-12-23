
using CustomizingOpenApi;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5305");

builder.Services.AddOpenApi(x =>
{
    // Change the OpenAPI version..
    x.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;

    x.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Contact = new OpenApiContact
        {
            Name = "Sander ten Brinke",
            Email = "s.tenbrinke2@gmail.com",
            Url = new Uri("https://stenbrinke.nl")
        };

        return Task.CompletedTask;
    });

    x.AddOperationTransformer<OpenApiInternalServerErrorOperationTransformer>();

    // Add operation-specific transformer that only applies to specific endpoints
    x.AddOperationTransformer<WeatherForecastOperationTransformer>();

    x.AddSchemaTransformer(new OpenApiDoubleToDecimalSchemaTransformer());
});

var app = builder.Build();

app.MapOpenApi();

app.MapWeatherEndpoints();

app.Run();


public class OpenApiInternalServerErrorOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        operation.Responses?.Add("500", new OpenApiResponse { Description = "Internal server error" });
        return Task.CompletedTask;
    }
}

/// <summary>
/// Operation-specific transformer that only applies to the GetWeatherForecast operation
/// </summary>
public class WeatherForecastOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        // Only apply this transformation to the GetWeatherForecast operation
        if (operation.OperationId == "GetWeatherForecast")
        {
            operation.Deprecated = false;
            
            // Add additional metadata specific to this operation
            operation.ExternalDocs = new OpenApiExternalDocs
            {
                Description = "Learn more about weather forecasts",
                Url = new Uri("https://example.com/weather-docs")
            };
        }

        return Task.CompletedTask;
    }
}

public class OpenApiDoubleToDecimalSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type == typeof(decimal))
        {
            schema.Format = "decimal";
        }
        return Task.CompletedTask;
    }
}
