using Microsoft.Extensions.Caching.Memory;

namespace SympliSeoChecker.Service.Caching
{
    public class MemoryCacheService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache) => _memoryCache = memoryCache;

        public void Set<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow)
            => _memoryCache.Set(key, value, absoluteExpirationRelativeToNow);

        public bool TryGet<T>(string key, out T value) => _memoryCache.TryGetValue(key, out value);
    }
}
