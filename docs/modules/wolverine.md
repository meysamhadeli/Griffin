# Wolverine

`Griffin.Wolverine` configures Wolverine with durable PostgreSQL message storage, EF Core transaction integration, durable local queues, and optional RabbitMQ transport.

## Installation

```bash
dotnet add package Griffin.Wolverine
```

This package references `Griffin.Core` and `Griffin.Web`. A reachable PostgreSQL database is required for message persistence with **both** `RabbitMq` and `InMemory` transport modes.

## Registration

```csharp
using Griffin.Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomWolverine(
    builder.Environment,
    TransportType.RabbitMq,
    "messaging",
    typeof(Program).Assembly);

var app = builder.Build();
app.Run();
```

Pass every assembly containing application message handlers. Griffin also includes its own internal-command handler assembly. Handler discovery follows Wolverine's conventions.

Use `TransportType.InMemory` to omit the external broker. This does **not** disable PostgreSQL persistence or create a database-free test mode.

## Configuration

Configure named connections through your application's secret store or environment:

```text
ConnectionStrings__messaging=<PostgreSQL connection string>
ConnectionStrings__rabbitmq=<RabbitMQ connection string>
```

The persistence connection is resolved in this order:

1. The explicit `persistenceConnectionName`, its kebab-case variant, or the corresponding nested `PostgresOptions:ConnectionString` key.
2. `ConnectionStrings:wolverine`.
3. `ConnectionStrings:postgres`.
4. `PostgresOptions:ConnectionString`.

Registration fails when no persistence connection string is available. Griffin appends `Keepalive=30;Tcp Keepalive=true` unless the connection string already contains `Keepalive=`.

For RabbitMQ, `ConnectionStrings:rabbitmq` takes precedence. Otherwise configure `RabbitMqOptions:HostName`, `UserName`, `Password`, and optional `Port` (default `5672`). Although `RabbitMqOptions` exposes `ExchangeName`, this registration uses Wolverine's conventional routing rather than reading that property.

## Sending and Handling Messages

```csharp
using Wolverine;

public record OrderSubmitted(Guid OrderId);

public static class OrderSubmittedHandler
{
    public static void Handle(OrderSubmitted message)
    {
        Console.WriteLine($"Order received: {message.OrderId}");
    }
}
```

Inject `IMessageBus` into an application endpoint or service to send messages:

```csharp
await bus.PublishAsync(new OrderSubmitted(orderId));
```

`bus` is an injected `IMessageBus`; `orderId` is your application's order identifier. Publishing and handler routing follow Wolverine's transport and discovery conventions, not MediatR request dispatch.

## Durability and Failures

- All listeners use a durable inbox; all sending endpoints use a durable outbox.
- Local queues are durable, and conventional local routing is additive.
- EF Core transaction integration is enabled. Coordinate application transactions through Wolverine's documented transaction/outbox patterns; registration alone is not a transaction around arbitrary application code.
- Inbox and outbox stale intervals are five minutes.
- Griffin's `ValidationException` moves messages to the error queue.
- The general exception policy retries with cooldowns of 200, 400, and 800 milliseconds.
- RabbitMQ resources are auto-provisioned. Ensure the broker identity has appropriate permissions.

In an environment named `test`, Griffin stubs external transports. PostgreSQL message persistence is still configured.

## Related Documentation

- [Griffin source](https://github.com/meysamhadeli/Griffin/tree/main/src/Wolverine)
- [Wolverine documentation](https://wolverinefx.io/)
- [Core contracts](/modules/core)