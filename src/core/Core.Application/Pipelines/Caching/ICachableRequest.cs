namespace Core.Application.Pipelines.Caching;

public interface ICachableRequest
{
    string CacheKey { get; } // GetProductsList 
    bool ByPassCache { get; } // true
    string? CacheGroupKey { get; } // Products
    TimeSpan? SlidingExpiration { get; }
}