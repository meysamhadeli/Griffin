# EventStoreDB

`Griffin.EventStoreDB` provides event-sourced aggregate storage, serialization, projections, subscriptions, and checkpoint support.

## Installation

```bash
dotnet add package Griffin.EventStoreDB
```

An EventStoreDB server compatible with the package's gRPC client is required at runtime.

## Registration

```csharp
using Griffin.EventStoreDB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventStore(
    builder.Configuration,
    typeof(Program).Assembly);
```

`AddEventStore` registers options, the EventStoreDB client, aggregate repositories, and projection processors from the supplied assemblies. With no explicit assemblies, it scans the entry assembly.

## Configuration

Use `ConnectionStrings:eventstore`, or the fallback `EventStoreOptions:ConnectionString`:

```text
ConnectionStrings__eventstore=<EventStoreDB connection string>
```

Configure server TLS, credentials, and certificate trust for the target environment. Do not disable TLS in production to copy a local development connection string.

## Event-Sourced Aggregates

The `Griffin.EventStoreDB.Events` namespace contains `AggregateEventSourcing<TId>` and `IAggregateEventSourcing<TId>`. The repository works with GUID-identified event-sourced aggregates, not arbitrary EF entities.

Inject `IEventStoreDBRepository<YourAggregate>` from `Griffin.EventStoreDB.Repository`, where `YourAggregate` is your application's event-sourced aggregate type.

- `Find(id, cancellationToken)` rehydrates an aggregate from its stream.
- `Add(aggregate, cancellationToken)` expects a new stream.
- `Update(aggregate, expectedRevision, cancellationToken)` appends pending events with optimistic concurrency.
- `Delete(...)` currently delegates to `Update`; model deletion as an event rather than assuming the stream is removed.

Persisting clears pending aggregate events before append completion. Design failure recovery carefully; do not assume the same aggregate instance retains its pending events after a failed write.

## Projections and Subscriptions

Projection registration scans for `IProjectionProcessor` implementations. Registration alone does not start a subscription worker.

Add the hosted subscription explicitly when your service should consume all-stream events:

```csharp
using Griffin.EventStoreDB;

builder.Services.AddEventStoreDBSubscriptionToAll();
```

The default uses EventStoreDB-backed checkpoints. The lower-level `AddEventStoreDB` method accepts `EventStoreDBOptions` for checkpoint registration. If internal checkpoint registration is disabled, provide an `ISubscriptionCheckpointRepository` implementation yourself.

Provision permissions for the subscribed streams and checkpoint storage. Make projection handlers safe to replay and resilient to duplicate delivery.

## Related Documentation

- [EventStoreDB source](https://github.com/meysamhadeli/Griffin/tree/main/src/EventStoreDB)
- [EventStoreDB client documentation](https://docs.kurrent.io/clients/grpc/)
- [Core event contracts](/modules/core)