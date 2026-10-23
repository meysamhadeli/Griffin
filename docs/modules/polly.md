# Polly

`Griffin.Polly` exposes a synchronous retry helper built on the Polly library.

## Installation

```bash
dotnet add package Griffin.Polly
```

## Retry on Failure

```csharp
using Griffin.Polly;
using Microsoft.Extensions.Logging;

Extensions.Logger = loggerFactory.CreateLogger("Griffin.Polly");

var result = someObject.RetryOnFailure(() => riskyOperation(), retryCount: 3);
```

`RetryOnFailure` retries any exception up to `retryCount` times (default `3`) and returns the operation result. Set `Griffin.Polly.Extensions.Logger` before use; the helper logs each attempt and exception through it, and a null logger causes failures while retrying.

The helper is synchronous and does not include backoff or an async overload. Use Polly's own policies when you need asynchronous retries, circuit breakers, or custom delay strategies.

## Related Documentation

- [Polly source](https://github.com/meysamhadeli/Griffin/tree/main/src/Polly)
- [Polly](https://www.thepollyproject.org/)
