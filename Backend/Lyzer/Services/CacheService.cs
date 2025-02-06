using Lyzer.Clients;

using StackExchange.Redis;

namespace Lyzer.Services
{
    public class CacheService
    {
        private readonly ILogger<CacheService> _logger;
        private readonly ConnectionMultiplexer _cache;
        private readonly IDatabase _db;

        public CacheService(ILogger<CacheService> logger)
        {
            string host = Environment.GetEnvironmentVariable("CACHE_HOST") ?? "127.0.0.1";
            _logger = logger;
            _cache = ConnectionMultiplexer.Connect(host);
            _db = _cache.GetDatabase();
        }

        public async Task Add(string key, string value, TimeSpan? ttl)
        {
            await _db.StringSetAsync(key, value, ttl);
        }

        public async Task Remove(string key)
        {
            await _db.KeyDeleteAsync(key);
        }

        public async Task<string?> Get(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task<bool?> exists(string key)
        {
            return await _db.KeyExistsAsync(key);
        }
    }
}