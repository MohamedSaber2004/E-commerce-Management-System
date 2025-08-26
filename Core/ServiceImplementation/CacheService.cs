
using Domain_Layer.Repository_Interfaces;
using Service_Abstraction;
using System.Text.Json;

namespace Service_Implementation
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey) => await cacheRepository.GetAsync(CacheKey);

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(CacheValue);
            await cacheRepository.SetAsync(CacheKey, value, TimeToLive);
        }
    }
}
