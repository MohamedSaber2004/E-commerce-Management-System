using Domain_Layer.Models.BasketModule;
using StackExchange.Redis;
using System.Text.Json;

namespace Presistence_Layer.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(customerBasket);
            var isCreatedOrUpdated =  await _database.StringSetAsync(customerBasket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            if (isCreatedOrUpdated)
            {
                return await GetBasketAsync(customerBasket.Id);
            }
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        =>  await _database.KeyDeleteAsync(id);


        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var Basket = await _database.StringGetAsync(Key);
            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}
