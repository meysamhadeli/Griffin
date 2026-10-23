# Caching

`Griffin.Caching` provides MediatR pipeline behaviors for response caching and cache invalidation backed by EasyCaching.

## Installation

```bash
dotnet add package Griffin.Caching
```

Register EasyCaching in your application. Both behaviors resolve `IEasyCachingProviderFactory` and use the provider named `mem`; configure that provider before requests flow through the pipeline.

## Cache a Request

Implement `ICacheRequest` on requests whose responses should be cached:

```csharp
using Griffin.Caching;
using MediatR;

public sealed record GetProduct(Guid Id) : IRequest<Product>, ICacheRequest
{
    public string CacheKey => $"product-{Id}";
    public DateTime? AbsoluteExpirationRelativeToNow => null;
}
```

Register `CachingBehavior<,>` as a MediatR pipeline behavior in your service collection. When a request implements `ICacheRequest`:

- `CacheKey` identifies the cached response.
- A cache hit short-circuits the handler and returns the stored value.
- On a miss, the handler runs and the response is stored with the configured expiration.

The contract's `AbsoluteExpirationRelativeToNow` is currently consumed as a time-based expiration value rather than a true relative span. The behavior defaults to one hour when it is `null`. Verify your effective expiration before relying on precise cache lifetime, and prefer explicit cache keys that include all request discriminator values.

## Invalidate a Cache Entry

Implement `IInvalidateCacheRequest` on commands that should remove a cached entry after executing:

```csharp
using Griffin.Caching;
using MediatR;

public sealed record DeleteProduct(Guid Id) : IRequest, IInvalidateCacheRequest
{
    public string CacheKey => $"product-{Id}";
}
```

Register `InvalidateCachingBehavior<,>`. It runs the handler, then removes the matching cache key. Invalidation happens after a successful handler execution; a failed request does not reach the removal call.

Both behaviors fall through unchanged for requests that do not implement the relevant interface.

## Related Documentation

- [Caching source](https://github.com/meysamhadeli/Griffin/tree/main/src/Caching)
- [EasyCaching](https://github.com/dotnetcore/EasyCaching)
- [MediatR pipeline behaviors](https://github.com/jbogard/MediatR/wiki/Behaviors)
