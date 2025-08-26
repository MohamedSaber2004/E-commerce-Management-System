using Domain_Layer.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Repository_Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Key);

        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? TimeToLive = null);

        Task<bool> DeleteBasketAsync(string id);
    }
}
