# EF Core

`Griffin.EFCore` integrates Entity Framework Core with PostgreSQL, context registration, migrations, seeding, aggregate auditing, and transaction helpers.

## Installation

```bash
dotnet add package Griffin.EFCore
```

A reachable PostgreSQL database is required when the application uses this integration.

## Define and Register a Context

```csharp
using Griffin.EFCore;
using Microsoft.EntityFrameworkCore;

public sealed class OrdersDbContext(DbContextOptions<OrdersDbContext> options)
    : AppDbContextBase(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersDbContext).Assembly);
    }
}
```

Add your entity sets and model configurations to this application-owned context, then register it:

```csharp
using Griffin.EFCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddCustomDbContext<OrdersDbContext>("orders");
```

The context must inherit `DbContext` and implement `IDbContext`; `AppDbContextBase` implements that contract. Registration exposes the context through `IDbContext` and adds the seed manager.

## Configuration

Set `ConnectionStrings:orders` through application configuration, or use `PostgresOptions:ConnectionString` as the fallback. Named connection lookup uses the kebab-case form of the supplied name.

```text
ConnectionStrings__orders=<PostgreSQL connection string>
```

The integration uses Npgsql, snake_case naming, and migrations from the context's assembly. It enables Npgsql legacy timestamp behavior and suppresses EF's pending-model-changes warning. Generate and review real migrations; registration is not a replacement for schema-change review.

## Migrations and Seeding

After building the host, `app.UseMigration<OrdersDbContext>()` applies pending migrations and invokes the seed manager. This is a startup operation that contacts the database and blocks until complete.

Use it only when startup-time migrations fit your deployment model. For multiple replicas or restricted database identities, prefer a controlled migration job. Application seeders should be idempotent.

## Aggregate Persistence

`AppDbContextBase.SaveChangesAsync` updates audit fields on tracked `IAggregate` instances. Modified/deleted aggregates increment `Version`; deleted aggregates become modified records with `IsDeleted = true`.

Soft-delete **filtering** is separate from marking an entity deleted. Apply appropriate EF query filters in your model, or use `FilterSoftDeletedProperties` for compatible aggregate mappings. Call the base `OnModelCreating` to retain date-time conversion behavior. Configure domain-event properties so EF does not attempt to persist interface-based event collections.

`GetDomainEvents()` collects and clears tracked aggregate events; it does not publish them by itself.

## Transactions and Pipeline Behavior

The base context exposes transaction methods and `ExecuteTransactionalAsync`. Registering the context does not automatically add `EfTxBehavior<,>` to MediatR.

If you opt into that behavior, register its `IEventDispatcher` dependency and examine its event-driven save path: the current implementation returns without calling `SaveChangesAsync` when no domain events exist. Do not rely on it to persist every command automatically. Keep save/transaction ownership explicit in application handlers.

## Related Documentation

- [EF Core source](https://github.com/meysamhadeli/Griffin/tree/main/src/EFCore)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [Core aggregates](/modules/core)
- [Wolverine transactions and messaging](/modules/wolverine)