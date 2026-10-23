# TestBase

`Griffin.TestBase` supplies integration-test fixtures and Testcontainers support for downstream test projects.

## Installation

```bash
dotnet add package Griffin.TestBase
```

This package is support infrastructure for your own test projects. It is not an executable test project and does not provide its own test suite.

## Test Fixture

`TestFixture<TEntryPoint>` wraps `WebApplicationFactory<TEntryPoint>` with container startup, fake JWT authentication, seeded data, and Wolverine message tracking. Derive your fixture from it and override `RegisterServices` to add test services:

```csharp
using Griffin.TestBase;

public sealed class OrdersApiFixture : TestFixture<Program>
{
    protected override void RegisterServices(Action<IServiceCollection> services)
    {
        base.RegisterServices(services);
        // replace or add test doubles here
    }
}
```

The fixture exposes `HttpClient` (with a fake bearer token), `ServiceProvider`, `Configuration`, a gRPC channel, and scope helpers. Implement `ITestDataSeeder` in your test project to seed data; the fixture discovers `ITestDataSeeder` implementations from application dependencies.

## Testcontainers

`TestContainers` provides factories for PostgreSQL, MongoDB, RabbitMQ, and EventStoreDB containers. Container names, ports, images, and credentials are configurable through the option types such as `PostgresContainerOptions` and `RabbitMqContainerOptions`.

```csharp
using Griffin.TestBase;

var postgres = TestContainers.PostgresTestContainer();
```

Docker must be available. The `TestFixture` starts containers during `InitializeAsync` and stops them on disposal.

## Related Documentation

- [TestBase source](https://github.com/meysamhadeli/Griffin/tree/main/src/TestBase)
- [Testcontainers for .NET](https://dotnet.testcontainers.org/)
- [xUnit](https://xunit.net/)
