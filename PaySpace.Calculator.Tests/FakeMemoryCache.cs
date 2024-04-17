using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace PaySpace.Calculator.Tests
{
    public class FakeMemoryCache : IMemoryCache
    {
        private readonly Dictionary<object, object> _cache = new Dictionary<object, object>();

        public ICacheEntry CreateEntry(object key)
        {
            return new FakeCacheEntry(key, _cache);
        }

        public void Dispose()
        {
        }

        public void Remove(object key)
        {
        }

        public bool TryGetValue(object key, out object value)
        {
            return _cache.TryGetValue(key, out value);
        }
    }

    public class FakeCacheEntry : ICacheEntry
    {
        private readonly Dictionary<object, object> _cache;
        private readonly object _key;

        public FakeCacheEntry(object key, Dictionary<object, object> cache)
        {
            _key = key;
            _cache = cache;
        }

        public object Key => _key;


        public void Dispose()
        {
        }

        public object Value
        {
            get => _cache[_key];
            set => _cache[_key] = value;
        }

        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }

        public IList<IChangeToken> ExpirationTokens { get; }

        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; }

        public CacheItemPriority Priority { get; set; }

        public long? Size { get; set; }
    }
}