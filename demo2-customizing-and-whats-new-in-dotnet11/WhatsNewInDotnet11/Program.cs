using Scalar.AspNetCore;
using WhatsNewInDotnet11;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5306");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();                               // /openapi/v1.json
app.MapOpenApi("/openapi/{documentName}.yaml"); // /openapi/v1.yaml
app.MapScalarApiReference();                    // /scalar

app.MapQueryEndpoints();
app.MapServerSentEventsEndpoints();
app.MapUnionEndpoints();
app.MapContentTypeEndpoints();
app.MapControllers();

app.Run();
