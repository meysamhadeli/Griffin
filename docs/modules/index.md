# Modules

Each Griffin package owns a focused integration. Install only the packages your application uses and register each integration explicitly.

## Messaging

- [Wolverine](./wolverine): durable messaging, PostgreSQL inbox/outbox, local queues, and RabbitMQ transport.

## Core and Web

- [Core](./core): CQRS contracts, domain models, events, exceptions, and pagination.
- [Web](./web): minimal endpoints, API versioning, correlation IDs, and options helpers.

## Persistence

- [EF Core](./efcore): PostgreSQL contexts, migrations, seeding, and transactions.
- [MongoDB](./mongo): contexts, repositories, and unit-of-work abstractions.
- [EventStoreDB](./eventstoredb): event-sourced storage, projections, and subscriptions.

## Pipeline Behaviors

- [Caching](./caching): request caching and invalidation behaviors.
- [Validation](./validation): FluentValidation pipeline behavior.
- [Logging](./log): request logging and slow-call warnings.
- [Polly](./polly): synchronous retry helper.

## Operations and Integrations

- [JWT](./jwt): bearer authentication and default policies.
- [OpenAPI](./openapi): OpenAPI, Swagger UI, and Scalar.
- [OpenTelemetry](./opentelemetrycollector): logs, metrics, and traces.
- [Health Check](./healthcheck): health endpoints and provider probes.
- [Problem Details](./problemdetails): RFC 7807 error responses.
- [Mapster](./mapster): mapping registration.

## Utilities and Testing

- [Utils](./utils): type discovery and synchronization helpers.
- [TestBase](./testbase): integration-test fixtures and Testcontainers.

## Shared Conventions

Griffin targets .NET 10. Package versions are shared across each release. Configuration values belong in application configuration; use environment variables or a secret store for credentials.

Examples describe Griffin's integration layer. Consult the linked upstream documentation for the full behavior of each underlying framework.