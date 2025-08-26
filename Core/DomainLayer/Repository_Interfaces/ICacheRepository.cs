
using System.Globalization;

namespace Domain_Layer.Repository_Interfaces
{
    public interface ICacheRepository
    {
        // Get
        Task<string?> GetAsync(string CacheKey);

        // Set
        Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive);
    }
}
