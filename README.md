<p align="center">
	<img src="assets/griffin-logo.png" alt="Griffin library logo" width="180">
</p>

<h1 align="center">Griffin</h1>

<p align="center"><a href="https://meysamhadeli.github.io/Griffin/"><strong>Live Documentation</strong></a></p>

<p align="center">Reusable .NET building blocks for production-ready web services.</p>

<p align="center">
	<img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square" alt=".NET 10">
	<img src="https://img.shields.io/badge/license-MIT-2ea44f?style=flat-square" alt="MIT License">
	<img src="https://img.shields.io/badge/AI--Ready-11%2F12-brightgreen?style=flat-square" alt="AI-Ready 11 of 12">
</p>

## About

Griffin is a collection of independent NuGet packages for ASP.NET Core, CQRS, domain-driven design, event-driven messaging, persistence, testing, and observability. It is not a full application template; a small CRUD API may need only `Griffin.Core`, `Griffin.Web`, and one persistence module.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL, MongoDB, EventStoreDB, or a message broker only when the corresponding module is used
- Docker is useful for integration tests and local infrastructure

## Installation

Each Griffin module is published as an independent NuGet package. All modules use the same version from each GitHub release. Install only the packages your service needs:

```bash
dotnet add package Griffin.Core
dotnet add package Griffin.Web
dotnet add package Griffin.EFCore
```

## Quick Start

The following example shows the usual ASP.NET Core composition. Add only the integrations required by your service.

```csharp
using Griffin.EFCore;
using Griffin.Jwt;
using Griffin.OpenApi;
using Griffin.ProblemDetails;
using Griffin.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomVersioning();
builder.Services.AddAspnetOpenApi();
builder.Services.AddJwt();
builder.Services.AddCustomHealthCheck();
builder.AddMinimalEndpoints();

// TContext must inherit DbContext and implement IDbContext.
builder.AddCustomDbContext<ApplicationDbContext>("database");

var app = builder.Build();

app.UseCorrelationId();
app.UseCustomProblemDetails();
app.UseAuthentication();
app.UseAuthorization();
app.MapMinimalEndpoints();
app.UseAspnetOpenApi();
app.UseCustomHealthCheck();
app.UseMigration<ApplicationDbContext>();

app.Run();
```

## Modules

| Project | Purpose |
| --- | --- |
| [`Griffin.Core`](https://meysamhadeli.github.io/Griffin/modules/core/) | Domain, CQRS, events, exceptions, and pagination contracts |
| [`Griffin.Web`](https://meysamhadeli.github.io/Griffin/modules/web/) | Minimal endpoints, API versioning, options, correlation IDs, and web helpers |
| [`Griffin.EFCore`](https://meysamhadeli.github.io/Griffin/modules/efcore/) | EF Core and PostgreSQL context, migration, seeding, and transaction helpers |
| [`Griffin.Mongo`](https://meysamhadeli.github.io/Griffin/modules/mongo/) | MongoDB contexts, repositories, and unit-of-work abstractions |
| [`Griffin.EventStoreDB`](https://meysamhadeli.github.io/Griffin/modules/eventstoredb/) | EventStoreDB configuration, projections, subscriptions, and repositories |
| [`Griffin.Wolverine`](https://meysamhadeli.github.io/Griffin/modules/wolverine/) | Wolverine messaging and application integration |
| [`Griffin.Caching`](https://meysamhadeli.github.io/Griffin/modules/caching/) | EasyCaching MediatR behaviors and cache contracts |
| [`Griffin.Validation`](https://meysamhadeli.github.io/Griffin/modules/validation/) | FluentValidation MediatR behavior |
| [`Griffin.Log`](https://meysamhadeli.github.io/Griffin/modules/log/) | Logging behavior for application requests |
| [`Griffin.Polly`](https://meysamhadeli.github.io/Griffin/modules/polly/) | Resilience and HTTP client helpers |
| [`Griffin.Jwt`](https://meysamhadeli.github.io/Griffin/modules/jwt/) | JWT bearer authentication and authorization policies |
| [`Griffin.OpenApi`](https://meysamhadeli.github.io/Griffin/modules/openapi/) | OpenAPI, Swagger UI, and Scalar setup |
| [`Griffin.OpenTelemetryCollector`](https://meysamhadeli.github.io/Griffin/modules/opentelemetrycollector/) | Logs, metrics, traces, and diagnostics setup |
| [`Griffin.HealthCheck`](https://meysamhadeli.github.io/Griffin/modules/healthcheck/) | Health and liveness endpoint configuration |
| [`Griffin.ProblemDetails`](https://meysamhadeli.github.io/Griffin/modules/problemdetails/) | Consistent HTTP exception and status-code responses |
| [`Griffin.Mapster`](https://meysamhadeli.github.io/Griffin/modules/mapster/) | Mapster registration and assembly scanning |
| [`Griffin.TestBase`](https://meysamhadeli.github.io/Griffin/modules/testbase/) | Shared test fixtures and integration-test support |
| [`Griffin.Utils`](https://meysamhadeli.github.io/Griffin/modules/utils/) | General-purpose shared utilities |

Each module has a dedicated documentation page covering installation, registration, configuration, and usage. See the [live documentation](https://meysamhadeli.github.io/Griffin/) for details.

## Development

```bash
dotnet restore griffin.slnx
dotnet build griffin.slnx
```

No test project is currently present; `Griffin.TestBase` provides fixtures for downstream integration tests. For contribution guidelines and the documentation build workflow, see [CONTRIBUTION.md](./CONTRIBUTION.md).

# Support

If you like my work, feel free to:

- ⭐ this repository. And we will be happy together :)

Thanks a bunch for supporting me!

## Contribution

Thanks to all [contributors](https://github.com/meysamhadeli/Griffin/graphs/contributors), you're awesome and this wouldn't be possible without you! The goal is to build a categorized, community-driven collection of very well-known resources.

Please follow this [contribution guideline](./CONTRIBUTION.md) to submit a pull request or create the issue.


## License

Griffin is available under the [MIT License](LICENSE).
