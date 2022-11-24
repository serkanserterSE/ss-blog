using Microsoft.Extensions.Caching.Distributed;
using SS.Blog.Cache.Abstractions;

namespace SS.Blog.Cache.Concretes
{
    public class RedisCache : ICache
    {
        private readonly IDistributedCache _cache;

        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetString(string key, CancellationToken cancellationToken)
        {
            return await _cache.GetStringAsync(key, cancellationToken);
        }

        public async Task SetString(string key, string value, CancellationToken cancellationToken)
        {
            await _cache.SetStringAsync(key, value, cancellationToken);
        }
    }
}
