# MongoDB

`Griffin.Mongo` supplies a MongoDB context, aggregate repositories, and unit-of-work/transaction abstractions.

## Installation

```bash
dotnet add package Griffin.Mongo
```

MongoDB is required at runtime. MongoDB transactions require a replica set or a compatible sharded deployment, not a standalone server.

## Context and Registration

```csharp
using Griffin.Mongo;
using Microsoft.Extensions.Options;

public sealed class OrdersMongoContext(IOptions<MongoOptions> options)
    : MongoDbContext(options);
```

Register the application context:

```csharp
using Griffin.Mongo;

var builder = WebApplication.CreateBuilder(args);
builder.AddMongoDbContext<OrdersMongoContext>();
```

Registration adds the context, `IMongoDbContext`, open-generic `IMongoRepository<,>`, and `IMongoUnitOfWork<>`. An `IServiceCollection` overload supports a separate context service interface and implementation.

## Configuration

```json
{
  "MongoOptions": {
    "DatabaseName": "orders"
  }
}
```

Supply the connection through `ConnectionStrings:mongo`, which takes precedence over `MongoOptions:ConnectionString`:

```text
ConnectionStrings__mongo=<MongoDB connection string>
```

The context consumes `IOptions<MongoOptions>`. Prefer configuration binding; the optional configurator currently configures named options, so it is not a replacement for the default options consumed by the context.

## Repositories

Repository entities must implement `IAggregate<TId>`. For example, an application read model can inherit `Aggregate<Guid>`:

```csharp
using Griffin.Core.Model;

public record OrderReadModel : Aggregate<Guid>
{
    public string Reference { get; set; } = string.Empty;
}
```

Inject `IMongoRepository<OrderReadModel, Guid>` into application services. Its implementation provides `FindByIdAsync`, `FindOneAsync`, `FindAsync`, `GetAllAsync`, `AddAsync`, `UpdateAsync`, and deletion methods.

Collection names default to the lowercase entity type name. Context serialization uses camelCase elements, string GUIDs and enums, and ignores extra document elements.

## Write and Transaction Boundaries

Current repository writes execute immediately. They do not enqueue operations into `MongoDbContext.AddCommand`, and they do not explicitly pass `context.Session` to driver operations. Calling `SaveChangesAsync` therefore does not make ordinary repository writes atomic.

For atomic multi-document operations, use MongoDB driver APIs with the active session explicitly and verify your transaction boundaries. The context's transaction helpers do not automatically enlist arbitrary operations that omit that session.

`RawQuery` is not implemented. `DeleteRangeAsync` currently calls a single-document delete, so it must not be treated as a bulk-delete API. Repository reads do not automatically filter `IsDeleted` records.

## Related Documentation

- [Mongo source](https://github.com/meysamhadeli/Griffin/tree/main/src/Mongo)
- [MongoDB .NET driver](https://www.mongodb.com/docs/drivers/csharp/current/)
- [Core models](/modules/core)