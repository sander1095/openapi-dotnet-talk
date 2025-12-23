# YAML Support Demo

This project demonstrates YAML support for OpenAPI in .NET 10.

## Features

- **JSON Output**: Access the OpenAPI specification in JSON format at `/openapi/v1.json`
- **YAML Output**: Access the OpenAPI specification in YAML format at `/openapi/v1.yaml`
- **Scalar UI**: Browse the API documentation at `/scalar/v1`

## Running the Project

```bash
cd demo3-yaml-support/YamlSupport
dotnet run
```

The API will be available at http://localhost:5306

## Endpoints

- `/weatherforecast` - Get weather forecast data
- `/openapi/v1.json` - OpenAPI specification in JSON format
- `/openapi/v1.yaml` - OpenAPI specification in YAML format
- `/scalar/v1` - Interactive API documentation
