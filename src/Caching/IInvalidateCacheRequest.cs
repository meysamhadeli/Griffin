namespace Griffin.Caching
{
    public interface IInvalidateCacheRequest
    {
        string CacheKey { get; }
    }
}