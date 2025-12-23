# .NET 10 Security Features for OpenAPI

This document outlines security-related features and considerations for OpenAPI in .NET 10.

## Automatic Security Features

### 1. HTTPS Redirection
.NET 10 projects created with the WebAPI template automatically include HTTPS redirection for enhanced security.

```csharp
app.UseHttpsRedirection();
```

### 2. CORS (Cross-Origin Resource Sharing)
While not automatic, CORS support is built-in and easily configurable:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://example.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
```

### 3. Authentication & Authorization
OpenAPI in .NET 10 can automatically document authentication schemes:

```csharp
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
        {
            ["Bearer"] = new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme."
            }
        };
        return Task.CompletedTask;
    });
});
```

### 4. API Key Authentication
Document API key requirements in OpenAPI:

```csharp
options.AddDocumentTransformer((document, context, cancellationToken) =>
{
    document.Components.SecuritySchemes["ApiKey"] = new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        In = OpenApiParameterLocation.Header,
        Name = "X-API-Key",
        Description = "API Key for authentication"
    };
    return Task.CompletedTask;
});
```

## Security Best Practices

### 1. Hide Sensitive Endpoints
Only expose OpenAPI in development environments:

```csharp
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
```

### 2. Input Validation
Use data annotations and model validation:

```csharp
public class CreateTalkModel
{
    [Required]
    [StringLength(200)]
    public required string Title { get; set; }
    
    [Range(1, 480)]
    public int LengthInMinutes { get; set; }
}
```

### 3. Rate Limiting (New in .NET 10)
.NET 10 includes built-in rate limiting support:

```csharp
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromMinutes(1);
    });
});
```

### 4. Security Headers
Add security headers to protect your API:

```csharp
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    await next();
});
```

## Note on Automatic Security

As of .NET 10, there isn't a single "automatic security" feature that's enabled by default specifically for OpenAPI. However, the framework provides:

1. **Better defaults**: HTTPS is the default for new projects
2. **Built-in tools**: Rate limiting, authentication, and authorization are first-class features
3. **OpenAPI integration**: Security schemes are easily documented in the OpenAPI specification

The comment in the issue about .NET 10/11 adding automatic security support likely refers to these incremental improvements rather than a single feature. Always review and customize security settings based on your application's specific requirements.

## References

- [ASP.NET Core Security](https://learn.microsoft.com/en-us/aspnet/core/security/)
- [OpenAPI Security](https://swagger.io/docs/specification/authentication/)
- [.NET 10 What's New](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10)
