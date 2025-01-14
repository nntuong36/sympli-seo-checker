namespace SympliSeoChecker.Service.Caching
{
    public interface ICachingService
    {
        void Set<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow);
        bool TryGet<T>(string key, out T value);
    }
}
