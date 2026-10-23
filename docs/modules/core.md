# Core

`Griffin.Core` provides application contracts and domain primitives shared by Griffin integrations. It does not configure a web host, database, or message transport.

## Installation

```bash
dotnet add package Griffin.Core
```

No registration extension is required for the contracts themselves. Register handlers and concrete services in your application.

## Commands and Queries

The `Griffin.Core.CQRS` namespace contains MediatR-based command, query, and handler contracts. `ICommand<T>` implements `IRequest<T>`; the non-generic `ICommand` returns MediatR's `Unit`.

```csharp
using Griffin.Core.CQRS;
using MediatR;

public record CreateOrder(string Reference) : ICommand<Guid>;

public sealed class CreateOrderHandler : IRequestHandler<CreateOrder, Guid>
{
    public Task<Guid> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}
```

This minimal handler illustrates the contract; add your own persistence and business rules. Register the handler assembly with MediatR before sending requests with `ISender`.

## Entities and Aggregates

`Entity<TId>` is an abstract record with `Id`, audit fields, `IsDeleted`, and `Version`. `Aggregate<TId>` adds a domain-event collection.

```csharp
using Griffin.Core.Model;

public record Order : Aggregate<Guid>
{
    public string Reference { get; set; } = string.Empty;
}
```

Use `AddDomainEvent` to enqueue an `IDomainEvent`. `ClearDomainEvents` returns pending events and removes them from the aggregate. Neither method persists nor publishes events by itself.

## Events

The event contracts distinguish domain events, integration events, and internal commands. `IEventDispatcher` exposes single-event and event-list `SendAsync` overloads. `IEventMapper` and `CompositeEventMapper` support mapping between application events.

Your application must register an appropriate dispatcher and mapping implementations. Installing Core alone does not select a broker or guarantee delivery. [Wolverine](/modules/wolverine) supplies a messaging integration; [EventStoreDB](/modules/eventstoredb) handles event-sourced storage.

## Exceptions and Pagination

`Griffin.Core.Exception` contains application exception types such as `NotFoundException`, `ConflictException`, and `ValidationException`. HTTP exception mapping belongs to [Problem Details](/modules/problem-details), not Core itself.

`Griffin.Core.Pagination` provides request, query, and page-list contracts. Applications still own query execution, ordering, and authorization.

## Related Documentation

- [Core source](https://github.com/meysamhadeli/Griffin/tree/main/src/Core)
- [MediatR](https://github.com/jbogard/MediatR)
- [Architecture](/architecture)