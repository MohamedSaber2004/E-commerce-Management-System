using StackExchange.Redis;

namespace Presistence_Layer.Repositories
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<string?> GetAsync(string CacheKey)
        {
            var cacheValue = await _database.StringGetAsync(CacheKey);
            return cacheValue.IsNullOrEmpty ? null : cacheValue.ToString();
        }

        public async Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive);
        }
    }
}
