# Web

`Griffin.Web` provides ASP.NET Core endpoint discovery, API versioning, correlation helpers, configuration binding, and service-registration utilities.

## Installation

```bash
dotnet add package Griffin.Web
```

Use this package from an ASP.NET Core application targeting .NET 10.

## Minimal Endpoints

Implement `IMinimalEndpoint` for each endpoint group:

```csharp
using Griffin.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

public sealed class GetOrdersEndpoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/orders", () => Results.Ok(Array.Empty<string>()));
        return builder;
    }
}
```

Register and map discovered endpoints in the host:

```csharp
using Griffin.Web;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.AddMinimalEndpoints(ServiceLifetime.Scoped, typeof(GetOrdersEndpoint).Assembly);
builder.Services.AddCustomVersioning();

var app = builder.Build();
app.UseCorrelationId();
app.MapMinimalEndpoints();
app.Run();
```

Explicit assemblies make discovery predictable. When omitted, Griffin discovers referenced and application-part assemblies. `MapMinimalEndpoints` creates a service scope and invokes `MapEndpoint` on each discovered implementation. Resolve per-request services through endpoint handler parameters rather than keeping scoped dependencies in endpoint instances.

## API Versioning

`AddCustomVersioning` defaults to version `1.0`, assumes that version when unspecified, and reads versions from the `api-version` header or URL segment. It reports supported/deprecated versions and configures versioned API exploration.

Pass an `Action<ApiVersioningOptions>` to customize the defaults. Your application must still associate endpoints/controllers with versions and define versioned route templates; registering versioning does not rewrite every route automatically.

## Correlation IDs

`UseCorrelationId` stores the incoming `correlationId` header, or a generated identifier, in `HttpContext.Items`. `HttpContext.GetCorrelationId()` parses that value as a GUID.

Send GUID-formatted correlation IDs if you call `GetCorrelationId`. Arbitrary strings are not validated by the middleware and can fail GUID parsing. The middleware does not automatically add a response header.

## Options and Service Utilities

`AddValidateOptions<TOptions>()` binds the section named after the options type, enables data-annotation validation, and registers the resolved options object. `GetOptions<TOptions>(section)` binds an explicitly named section.

Service replacement helpers support scoped, transient, and singleton registrations. They remove the first matching descriptor, not every registration of that type; take care with multi-registration services.

Authentication, OpenAPI, health checks, and error responses are separate opt-in packages.

## Related Documentation

- [Web source](https://github.com/meysamhadeli/Griffin/tree/main/src/Web)
- [JWT authentication](/modules/jwt)
- [OpenAPI](/modules/openapi)
- [Problem Details](/modules/problem-details)