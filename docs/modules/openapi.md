# OpenAPI

`Griffin.OpenApi` registers OpenAPI document generation, Swagger UI, and Scalar, with a security-scheme document transformer.

## Installation

```bash
dotnet add package Griffin.OpenApi
```

## Register and Map

```csharp
using Griffin.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAspnetOpenApi();

var app = builder.Build();
app.UseAspnetOpenApi();
app.Run();
```

`AddAspnetOpenApi` registers an `v1` OpenAPI document and adds `SecuritySchemeDocumentTransformer`. `UseAspnetOpenApi` maps the OpenAPI endpoint, configures a Swagger endpoint for each discovered API version, and maps Scalar.

Combine with [API versioning](/modules/web) so discovered versions map to Swagger endpoints. API version metadata on endpoints is what exposes multiple versions; OpenAPI registration alone documents `v1`.

## Related Documentation

- [OpenAPI source](https://github.com/meysamhadeli/Griffin/tree/main/src/OpenApi)
- [ASP.NET Core OpenAPI](https://learn.microsoft.com/aspnet/core/fundamentals/openapi/overview)
- [Scalar](https://scalar.com/)
