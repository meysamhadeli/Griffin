# Health Check

`Griffin.HealthCheck` configures ASP.NET Core health checks, optional provider probes, and a HealthChecks UI.

## Installation

```bash
dotnet add package Griffin.HealthCheck
```

## Register and Map

```csharp
using Griffin.HealthCheck;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomHealthCheck();

var app = builder.Build();
app.UseCustomHealthCheck();
app.Run();
```

`HealthOptions.Enabled` (default `true`) gates the extended checks and the UI. A `self` liveness check tagged `live` is always registered.

In development, `/health` and `/alive` are mapped; `/alive` filters to `live`-tagged checks. When enabled, the UI is mapped at `/health-ui`.

## Provider Probes

When enabled, the registration always adds a RabbitMQ probe and conditionally adds MongoDB, PostgreSQL, and EventStoreDB probes when their connection strings are configured. Missing provider settings may make the RabbitMQ probe fail or registration throw; configure RabbitMQ or disable health checks when those providers are not present.

Probes are added from the `Griffin.*` option types at startup, so provide connection values before `AddCustomHealthCheck` resolves them.

## Related Documentation

- [HealthCheck source](https://github.com/meysamhadeli/Griffin/tree/main/src/HealthCheck)
- [ASP.NET Core health checks](https://learn.microsoft.com/aspnet/core/host-and-deploy/health-checks)
