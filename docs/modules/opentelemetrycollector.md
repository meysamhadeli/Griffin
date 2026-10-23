# OpenTelemetry Collector

`Griffin.OpenTelemetryCollector` wires OpenTelemetry logging, metrics, and tracing with selectable exporters and ASP.NET Core instrumentation.

## Installation

```bash
dotnet add package Griffin.OpenTelemetryCollector
```

## Register Observability

```csharp
using Griffin.OpenTelemetryCollector;

var builder = WebApplication.CreateBuilder(args);
builder.AddCustomObservability();

var app = builder.Build();
app.UseCustomObservability();
app.Run();
```

## Configuration

`ObservabilityOptions` requires a non-empty `InstrumentationName`:

```json
{
  "ObservabilityOptions": {
    "InstrumentationName": "orders-api",
    "ServiceName": "orders-api",
    "MetricsEnabled": true,
    "TracingEnabled": true,
    "LoggingEnabled": true
  }
}
```

The default `ServiceName` falls back to the application name. Metrics, tracing, and logging can each be disabled; when all three are off, no OpenTelemetry SDK is configured.

## Exporters

Tracing supports OTLP, Jaeger (via OTLP), Zipkin, console, Aspire dashboard, and Grafana exporters through boolean flags and their option types. Logging exporters are configured through `AddLoggingExporters`. Metrics support a Prometheus scraping endpoint when `UsePrometheusExporter` is enabled.

Set only the exporters your environment consumes. `UseCustomObservability` disables HTTP metrics for `/metrics` and `/health`, and enables the Prometheus scraping endpoint when configured.

OpenTelemetry logging integrates with the default .NET logging providers. Serilog requires `WriteToProviders = true` to participate.

## Related Documentation

- [OpenTelemetry source](https://github.com/meysamhadeli/Griffin/tree/main/src/OpenTelemetryCollector)
- [OpenTelemetry .NET](https://opentelemetry.io/docs/languages/net/)
