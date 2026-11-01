# openapi-dotnet-talk

The demos for my talk **The best ways to use the latest OpenAPI features in .NET 11!**

The demos use `Microsoft.AspNetCore.OpenApi` in .NET 11, which generates OpenAPI 3.2 documents by default. An optional tour shows the Swashbuckle baseline in .NET 8. Demo 2 covers transformers and the new OpenAPI features in .NET 11: HTTP `QUERY`, hierarchical tags, `[Obsolete]` as `deprecated`, Server-Sent Events, C# unions and multiple content types per status code.

## Requirements

- The .NET 11 RC3 SDK (`11.0.100-rc.3.26522.148`). [global.json](global.json) selects it and rolls forward to newer .NET 11 SDKs.
- Optional: the ASP.NET Core 8 runtime for the `OpenApiInDotnet8` project, which only the optional .NET 8 tour uses.
- [VS Code](https://code.visualstudio.com/) with the [CodeTour](https://marketplace.visualstudio.com/items?itemName=vsls-contrib.codetour) extension for the guided tours.

Build everything with:

```shell
dotnet build OpenApiInDotnet.slnx
```

The build also generates `demo1-using-openapi/BuildTimeOpenApiDocumentGeneration/openapi-spec.json`, which demo 1 shows at the end.

## Demos

Each app listens on a fixed port. Run one with `dotnet run --project <folder>`, for example `dotnet run --project demo1-using-openapi/Controllers`.

| Demo                       | Project                                                         | URL                    |
| -------------------------- | --------------------------------------------------------------- | ---------------------- |
| .NET 8 (Swashbuckle)       | `demo1-using-openapi/OpenApiInDotnet8`                          | http://localhost:5300  |
| .NET 11 (Controllers)      | `demo1-using-openapi/Controllers`                               | http://localhost:5301  |
| .NET 11 (Minimal API)      | `demo1-using-openapi/MinimalApi`                                | http://localhost:5302  |
| AOT                        | `demo1-using-openapi/AheadOfTime`                               | http://localhost:5303  |
| Build-time generation      | `demo1-using-openapi/BuildTimeOpenApiDocumentGeneration`        | http://localhost:5304  |
| Customizing OpenAPI        | `demo2-customizing-and-whats-new-in-dotnet11/CustomizingOpenApi` | http://localhost:5305  |
| What's new in .NET 11      | `demo2-customizing-and-whats-new-in-dotnet11/WhatsNewInDotnet11` | http://localhost:5306  |

The .NET 11 apps serve their OpenAPI document at `/openapi/v1.json` and Scalar at `/scalar`. The Controllers and What's new apps also serve `/openapi/v1.yaml`, and the Controllers app has Swagger UI at `/swagger`. The .NET 8 app has Swagger UI at `/swagger`.

## Tours

The demos follow two CodeTours in [.tours](.tours), plus two extra tours:

- **Demo 1** ([demo-1.tour](.tours/demo-1.tour)): the .NET 11 controllers, minimal API, AOT and build-time projects.
- **Demo 2** ([demo-2.tour](.tours/demo-2.tour)): transformers, hierarchical tags and the new OpenAPI 3.2 features.
- **Response descriptions** ([response-descriptions.tour](.tours/response-descriptions.tour)): the ways to set response descriptions in Minimal APIs.
- **.NET 8 (Swashbuckle)** ([dotnet-8.tour](.tours/dotnet-8.tour)): optional, skipped in the talk. How OpenAPI worked in .NET 8, as a baseline.

Open the repository in VS Code, install the recommended CodeTour extension, and start a tour from the **CodeTour** panel in the Explorer. Steps marked **DO:** are things to do live, like running an app or sending a request.

## Starting the demos

`./start-demos.sh` builds the solution, starts the apps the tours run live and opens their pages in the browser, in tour order. Press Ctrl+C to stop the apps.

| Option         | Effect                                                                                       |
| -------------- | -------------------------------------------------------------------------------------------- |
| `--dotnet8`    | Also starts the .NET 8 app (5300) and opens its Swagger UI first, for the optional .NET 8 tour |
| `--all`        | Also starts MinimalApi (5302) and AheadOfTime (5303), which the tours only show as code      |
| `--no-browser` | Only starts the apps                                                                         |

## Backup screenshots

[docs/demo-backup](docs/demo-backup) has screenshots of each live part of the demos, in case something doesn't work on stage.

## Pre-built assets

Run `dotnet publish -r win-x64 /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true` to generate the pre-built assets.

## Earlier versions of this talk

The tags [`dotnet-9`](https://github.com/sander1095/openapi-dotnet-talk/tree/dotnet-9) and [`dotnet-10`](https://github.com/sander1095/openapi-dotnet-talk/tree/dotnet-10) contain the code from the .NET 9 and .NET 10 versions of this talk.
