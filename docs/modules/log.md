# Logging

`Griffin.Log` provides a MediatR pipeline behavior that logs request handling and flags slow responses.

## Installation

```bash
dotnet add package Griffin.Log
```

## Register the Behavior

Register `LoggingBehavior<,>` as a MediatR pipeline behavior in your service collection.

The behavior logs the request and response type names at information level before invoking the handler, then logs completion. When handling takes longer than three seconds, it emits a warning with the elapsed time.

Only the type names are logged, not request payloads. Use application-level logging if you need structured request data, and keep sensitive fields out of log output.

## Related Documentation

- [Log source](https://github.com/meysamhadeli/Griffin/tree/main/src/Log)
- [OpenTelemetry logging](/modules/opentelemetrycollector)
