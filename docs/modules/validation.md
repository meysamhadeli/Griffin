# Validation

`Griffin.Validation` supplies a MediatR pipeline behavior that runs FluentValidation validators on incoming requests.

## Installation

```bash
dotnet add package Griffin.Validation
```

## Register the Behavior

Register `ValidationBehavior<,>` as a MediatR pipeline behavior in your service collection, alongside your `IValidator<T>` implementations.

```csharp
using FluentValidation;

public sealed class CreateOrderValidator : AbstractValidator<CreateOrder>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Reference).NotEmpty();
    }
}
```

The behavior resolves `IValidator<TRequest>` from the service provider for each request. When a validator is present it calls the request validation; requests without a registered validator pass through unchanged.

## Failure Behavior

Validation failures throw `Griffin.Core.Exception.ValidationException` carrying the **first** validation error message. Later errors are not included. This exception can then be mapped to a client-facing response through [Problem Details](/modules/problem-details).

`HandleValidationAsync` is a convenience extension that performs `ValidateAsync` and throws the core `ValidationException`. Use it directly when you need the same validation behavior outside the MediatR pipeline.

Design validators to keep the most useful message first, or use a custom exception mapper if you must surface every error.

## Related Documentation

- [Validation source](https://github.com/meysamhadeli/Griffin/tree/main/src/Validation)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [Core exceptions](/modules/core)
