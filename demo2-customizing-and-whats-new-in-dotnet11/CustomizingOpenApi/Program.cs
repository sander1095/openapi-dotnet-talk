using CustomizingOpenApi;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5305");

builder.Services.AddOpenApi(x =>
{
    // .NET 11 generates OpenAPI 3.2 documents by default.
    // Switch to 3.1 for tooling that doesn't support 3.2 yet:
    // x.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;

    x.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Contact = new OpenApiContact
        {
            Name = "Sander ten Brinke",
            Email = "s.tenbrinke2@gmail.com",
            Url = new Uri("https://stenbrinke.nl")
        };

        // OpenAPI 3.2: hierarchical tags with "parent", "kind" and "summary"
        document.Tags ??= new HashSet<OpenApiTag>();
        document.Tags.Add(new OpenApiTag { Name = "Weather", Kind = "nav", Summary = "Weather forecasts and alerts" });

        foreach (var tag in document.Tags.Where(t => t.Name is "Forecasts" or "Alerts"))
        {
            tag.Parent = new OpenApiTagReference("Weather", document);
        }

        return Task.CompletedTask;
    });

    x.AddOperationTransformer<OpenApiInternalServerErrorOperationTransformer>();

    x.AddSchemaTransformer(new OpenApiDoubleToDecimalSchemaTransformer());
});

// Let's change this to strict to demonstrate the schema transformer
builder.Services.ConfigureHttpJsonOptions(x =>
    x.SerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict
);

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

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
