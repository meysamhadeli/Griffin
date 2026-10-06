namespace Griffin.Caching;

public interface ICacheRequest
{
    string CacheKey { get; }
    DateTime? AbsoluteExpirationRelativeToNow { get; }
}