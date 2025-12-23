# OpenAPI 3.0 vs 3.1.1 Comparison

This document highlights the key differences between OpenAPI 3.0 and 3.1.1 as demonstrated in this project.

## Version Changes

- **OpenAPI 3.0.1**: Used in the original demo (see `openapi-3.0.json`)
- **OpenAPI 3.1.1**: Current version used in CustomizingOpenApi and BuildTimeOpenApiDocumentGeneration

## Key Differences in 3.1.1

### 1. JSON Schema Compatibility
OpenAPI 3.1.1 is fully compatible with JSON Schema 2020-12, whereas 3.0 used a modified subset of JSON Schema.

### 2. Info Object Changes
The `info.license` object now requires only the `name` field (the `url` is optional).

### 3. Webhooks Support
OpenAPI 3.1 introduces webhooks, allowing APIs to describe callback operations.

### 4. Improved Schema Support
- Better support for `const` keyword
- Support for `$dynamicRef` and `$dynamicAnchor`
- Full JSON Schema Draft 2020-12 support

### 5. Examples
The `example` field is now deprecated in favor of `examples` (array format).

## How to Compare

1. **View OpenAPI 3.0 spec**: 
   - File: `demo1-using-openapi/ControllersDotnet9/openapi-3.0.json`
   - Version: `3.0.1`

2. **View OpenAPI 3.1.1 spec**:
   - File: `demo2-customizing-and-build-time-openapi/BuildTimeOpenApiDocumentGeneration/openapi-spec.json`
   - Version: `3.1.1`

3. **Generate new specs**:
   ```bash
   # Build to regenerate the 3.1.1 spec
   cd demo2-customizing-and-build-time-openapi/BuildTimeOpenApiDocumentGeneration
   dotnet build
   
   # View the generated spec
   cat openapi-spec.json | jq '.openapi'
   ```

## Setting OpenAPI Version in Code

In .NET 10, you can specify the OpenAPI version in your configuration:

```csharp
builder.Services.AddOpenApi(x =>
{
    // Use OpenAPI 3.1
    x.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
    
    // Or use OpenAPI 3.0 for backward compatibility
    // x.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
});
```

## Benefits of Upgrading to 3.1.1

1. **Better tooling support**: Many modern tools now prefer or require OpenAPI 3.1
2. **JSON Schema alignment**: Easier to work with standard JSON Schema validators
3. **Future-proof**: OpenAPI 3.1 is the current standard with ongoing support
4. **Enhanced expressiveness**: More accurate schema descriptions with full JSON Schema support
