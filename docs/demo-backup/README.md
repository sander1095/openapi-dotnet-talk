# Demo backup screenshots

Screenshots of the running demos, for when a live demo doesn't cooperate. The file names follow the CodeTour
step they belong to (`demo1-09-*` is step 9 of `.tours/demo-1.tour`). All screenshots are 1600x900.

## Demo 1

| Screenshot | What it shows |
| --- | --- |
| [demo1-09-openapi-json.png](demo1-09-openapi-json.png) | `Controllers` at http://localhost:5301/openapi/v1.json, with `"openapi": "3.2.0"` at the top. |
| [demo1-09-scalar.png](demo1-09-scalar.png) | `Controllers` in Scalar at http://localhost:5301/scalar, with the OpenAPI 3.2.0 badge. |
| [demo1-09-swagger.png](demo1-09-swagger.png) | `Controllers` in Swagger UI at http://localhost:5301/swagger, reading the same `/openapi/v1.json` (OAS 3.2). |
| [demo1-10-minimalapi-scalar.png](demo1-10-minimalapi-scalar.png) | `MinimalApi` in Scalar at http://localhost:5302/scalar. |

## Demo 2

| Screenshot | What it shows |
| --- | --- |
| [demo2-09-customizing-nested-tags.png](demo2-09-customizing-nested-tags.png) | `CustomizingOpenApi` in Scalar: `Alerts` and `Forecasts` nested under the `Weather` tag, and the struck-through deprecated `/old-endpoint`. |
| [demo2-09-whatsnew-query-operation.png](demo2-09-whatsnew-query-operation.png) | `WhatsNewInDotnet11` in Scalar on the `QUERY /api/talks/search` operation, with its request body and curl example. |
| [demo2-09-whatsnew-query-sent.png](demo2-09-whatsnew-query-sent.png) | Scalar's client after sending `QUERY /api/talks/search` with `{ "title": "open" }`: 200 OK with the matching talk. |
| [demo2-10-whatsnew-sse.png](demo2-10-whatsnew-sse.png) | `GET /api/talks/live`: the `text/event-stream` response described as a stream item with `data`, `event` and `id`. |
| [demo2-11-whatsnew-union.png](demo2-11-whatsnew-union.png) | `GET /api/talks/{id}/session`: the `Session` union response as "Any of" `TalkModel` and `Workshop`. |
