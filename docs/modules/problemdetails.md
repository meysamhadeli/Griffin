# Problem Details

`Griffin.ProblemDetails` maps status codes and exceptions to RFC 7807 problem responses.

## Installation

```bash
dotnet add package Griffin.ProblemDetails
```

## Map Responses

```csharp
using Griffin.ProblemDetails;

var app = builder.Build();
app.UseCustomProblemDetails();
app.Run();
```

`UseCustomProblemDetails` adds status-code pages and an exception handler that both write `application/problem+json`.

## Exception Mapping

The handler maps Griffin exception types to status codes:

| Exception | Status |
| --- | --- |
| `ConflictException` | 409 |
| `ValidationException` | its configured `StatusCode` |
| `BadRequestException`, `AppException` | 400 |
| `NotFoundException` | 404 |
| `DbUpdateConcurrencyException` | 409 |
| `RpcException` | 400 |
| anything else | 500 |

In development, a serialized `exception` extension is added to the problem response. Configure your logging and telemetry separately; this middleware shapes responses but does not record exceptions.

## Related Documentation

- [ProblemDetails source](https://github.com/meysamhadeli/Griffin/tree/main/src/ProblemDetails)
- [Core exceptions](/modules/core)
