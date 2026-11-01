using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5301");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();                               // /openapi/v1.json
app.MapOpenApi("/openapi/{documentName}.yaml"); // /openapi/v1.yaml
app.MapScalarApiReference();                    // /scalar
app.UseSwaggerUI(x => x.SwaggerEndpoint("/openapi/v1.json", "Controllers | v1")); // /swagger

app.UseAuthorization();

app.MapControllers();

app.Run();