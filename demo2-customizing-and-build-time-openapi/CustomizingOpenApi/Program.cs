
using CustomizingOpenApi;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5305");

builder.Services.AddOpenApi(x =>
{
    // You can change the OpenAPI version if you want.
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

    x.AddSchemaTransformer(new OpenApiDoubleToDecimalSchemaTransformer());
});

// Let's change this to strict to demonstrate the schema transformer
builder.Services.ConfigureHttpJsonOptions(x =>
    x.SerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict
);

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
