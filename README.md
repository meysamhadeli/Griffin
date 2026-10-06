<p align="center">
	<img src="assets/griffin-logo.png" alt="Griffin library logo" width="180">
</p>

<h1 align="center">Griffin</h1>

<p align="center">Reusable .NET building blocks for production-ready web services.</p>

<p align="center">Griffin provides composable infrastructure for ASP.NET Core, CQRS, domain-driven design, event-driven messaging, persistence, testing, and observability.</p>

<p align="center">
	<img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square" alt=".NET 10">
	<img src="https://img.shields.io/badge/license-MIT-2ea44f?style=flat-square" alt="MIT License">
	<img src="https://img.shields.io/badge/AI--Ready-11%2F12-brightgreen?style=flat-square" alt="AI-Ready 11 of 12">
</p>

## Contents

- [What Griffin Provides](#what-griffin-provides)
- [Goals](#goals)
- [Technology](#technology)
- [When to Use Griffin](#when-to-use-griffin)
- [Requirements](#requirements)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Modules](#modules)
- [Configuration](#configuration)
- [Development](#development)
- [Support](#support)
- [Contribution](#contribution)
- [License](#license)

## What Griffin Provides

- CQRS contracts and domain model primitives for commands, queries, entities, aggregates, events, and pagination.
- Vertical-slice-friendly ASP.NET Core conventions for minimal endpoints, API versioning, correlation IDs, and service registration.
- Cross-cutting MediatR behaviors for validation, logging, transactions, caching, and cache invalidation.
- PostgreSQL and Entity Framework Core helpers for snake_case naming, migrations, seeding, soft-delete filters, and resilient transactions.
- MongoDB repository and unit-of-work abstractions for read models and document persistence.
- EventStoreDB integration with projections, subscriptions, event mapping, and event-sourced aggregates.
- Wolverine integration for messaging, durable workflows, local queues, and transport-backed application services.
- JWT authentication, OpenAPI and Scalar documentation, health checks, Problem Details, and OpenTelemetry.
- Shared testing infrastructure and utilities for integration tests and container-backed dependencies.

## Goals

Griffin aims to make the first version of a service easier to assemble without forcing every service into the same architecture. Its main goals are:

- Keep domain and application contracts independent from transport and persistence details.
- Make vertical slices easy to register, validate, observe, and test.
- Provide consistent defaults for common ASP.NET Core concerns.
- Support both synchronous request handling and durable asynchronous workflows.
- Keep infrastructure integrations optional and separated by project.

## Technology

Griffin builds on established .NET libraries rather than replacing them:

| Area | Technologies |
| --- | --- |
| Runtime and web | [.NET 10](https://dotnet.microsoft.com/download/dotnet/10.0), ASP.NET Core, Minimal APIs |
| Application | [MediatR](https://github.com/jbogard/MediatR), FluentValidation, Scrutor, Mapster |
| Data | [Entity Framework Core](https://github.com/dotnet/efcore), PostgreSQL, [MongoDB](https://www.mongodb.com/docs/drivers/csharp/current/), EventStoreDB |
| Messaging | [Wolverine](https://wolverinefx.io/), RabbitMQ |
| Operations | OpenTelemetry, health checks, Problem Details, Polly |
| API tooling | ASP.NET API Versioning, OpenAPI, Swagger UI, Scalar |
| Testing | xUnit, Testcontainers, Respawn, NSubstitute |

## When to Use Griffin

Griffin is a good fit when several services share the same web and application conventions, especially when a system needs CQRS, PostgreSQL-backed writes, MongoDB read models, event sourcing, durable messaging, or consistent observability.

It is intentionally not a full application template. A small CRUD API may need only `Griffin.Core`, `Griffin.Web`, and one persistence module. Avoid referencing the umbrella project when a narrower project reference is enough.

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

When a GitHub release is published, the publish workflow pushes every library package to NuGet.org using that release tag's version. The repository requires an Actions secret named `NUGET_API_KEY` containing a NuGet.org API key with package push permissions.

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
| `Griffin.Core` | Domain, CQRS, events, exceptions, and pagination contracts |
| `Griffin.Web` | Minimal endpoints, API versioning, options, correlation IDs, and web helpers |
| `Griffin.EFCore` | EF Core and PostgreSQL context, migration, seeding, and transaction helpers |
| `Griffin.Mongo` | MongoDB contexts, repositories, and unit-of-work abstractions |
| `Griffin.EventStoreDB` | EventStoreDB configuration, projections, subscriptions, and repositories |
| `Griffin.Wolverine` | Wolverine messaging and application integration |
| `Griffin.Caching` | EasyCaching MediatR behaviors and cache contracts |
| `Griffin.Validation` | FluentValidation MediatR behavior |
| `Griffin.Log` | Logging behavior for application requests |
| `Griffin.Polly` | Resilience and HTTP client helpers |
| `Griffin.Jwt` | JWT bearer authentication and authorization policies |
| `Griffin.OpenApi` | OpenAPI, Swagger UI, and Scalar setup |
| `Griffin.OpenTelemetryCollector` | Logs, metrics, traces, and diagnostics setup |
| `Griffin.HealthCheck` | Health and liveness endpoint configuration |
| `Griffin.ProblemDetails` | Consistent HTTP exception and status-code responses |
| `Griffin.Mapster` | Mapster registration and assembly scanning |
| `Griffin.TestBase` | Shared test fixtures and integration-test support |
| `Griffin.Utils` | General-purpose shared utilities |

Each library declares the NuGet packages it uses directly. Project references connect library APIs and are kept local to the modules that consume them.

## Configuration

Options are bound by type name in the standard .NET configuration system. A typical service keeps settings in `appsettings.json` or environment variables:

```json
{
	"Jwt": {
		"Authority": "https://identity.example.com",
		"Audience": "orders-api"
	},
	"PostgresOptions": {
		"ConnectionString": "Host=localhost;Database=orders;Username=postgres;Password=postgres"
	}
}
```

Prefer environment variables or a secret store for credentials in deployed environments. Do not commit connection strings, signing keys, or broker credentials.

## Development

Restore and build the complete library from the repository root:

```bash
dotnet restore griffin.slnx
dotnet build griffin.slnx
```

There is no test project in the solution currently. `Griffin.TestBase` provides shared fixtures and Testcontainers support for downstream integration tests.

Format changed C# files with the normal .NET formatter when needed:

```bash
dotnet format --no-restore
```

When developing integrations locally, Docker containers are recommended for PostgreSQL, MongoDB, EventStoreDB, and RabbitMQ. The test support projects include Testcontainers dependencies for repeatable integration environments.

# Support

If you like my work, feel free to:

- ⭐ this repository. And we will be happy together :)

Thanks a bunch for supporting me!

## Contribution

Thanks to all [contributors](https://github.com/meysamhadeli/Griffin/graphs/contributors), you're awesome and this wouldn't be possible without you! The goal is to build a categorized, community-driven collection of very well-known resources.

Please follow this [contribution guideline](./CONTRIBUTION.md) to submit a pull request or create the issue.


## License

Griffin is available under the [MIT License](LICENSE).
