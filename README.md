# openapi-dotnet-talk
My demos for my OpenAPI in .NET talk - Updated for .NET 10!

## What's New in .NET 10

- **XML Comments Support**: Summary and descriptions can now be specified using XML comments instead of attributes
- **ProducesResponseType Descriptions**: Add descriptions directly to ProducesResponseType attributes
- **OpenAPI 3.1.1 Support**: Updated to the latest OpenAPI specification version
- **YAML Support**: Generate OpenAPI documents in both JSON and YAML formats
- **Operation-Specific Transformers**: Apply transformations to specific endpoints

## Building and Running

Run `dotnet publish -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true` to generate the pre-built assets

## Demo Projects

| Demo                   | URL                    | Description                                    |
| ---------------------- | ---------------------- | ---------------------------------------------- |
| .NET 8                 | http://localhost:5300  | Legacy .NET 8 with Swashbuckle                 |
| .NET 10 (Controllers)  | http://localhost:5301  | Controllers with XML comments                  |
| .NET 10 (Minimal API)  | http://localhost:5302  | Minimal APIs with XML comments                 |
| AOT                    | http://localhost:5303  | Ahead-of-Time compilation                      |
| ---------------------- | ---------------------- | ---------------------------------------------- |
| Build-Time Generation  | http://localhost:5304  | Generate OpenAPI at build time                 |
| Customizing OpenAPI    | http://localhost:5305  | OpenAPI 3.1.1 with custom transformers         |
| YAML Support           | http://localhost:5306  | YAML and JSON OpenAPI document generation      |

## Key Features by Project

### Demo 1: Using OpenAPI
- **OpenApiInDotnet8**: Traditional Swashbuckle implementation (now on .NET 10)
- **ControllersDotnet9**: Controller-based APIs with ProducesResponseType descriptions
- **MinimalApiDotnet9**: Minimal APIs with XML comment support
- **AheadOfTime**: AOT-compatible OpenAPI implementation

### Demo 2: Customization
- **BuildTimeOpenApiDocumentGeneration**: Generate OpenAPI specs during build
- **CustomizingOpenApi**: 
  - OpenAPI 3.1.1 support
  - Document transformers for global changes
  - Operation-specific transformers for targeted customization
  - Schema transformers for type mapping

### Demo 3: YAML Support
- **YamlSupport**: 
  - JSON output at `/openapi/v1.json`
  - YAML output at `/openapi/v1.yaml`
  - Scalar UI for interactive documentation

## Running All Demos

```bash
# Build all projects
dotnet build OpenApiInDotnet10.sln

# Run individual projects
cd demo1-using-openapi/MinimalApiDotnet9 && dotnet run
cd demo2-customizing-and-build-time-openapi/CustomizingOpenApi && dotnet run
cd demo3-yaml-support/YamlSupport && dotnet run
```

