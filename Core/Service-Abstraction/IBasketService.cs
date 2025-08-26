
using Shared.DataTransferObjects.BasketModuleDTos;

namespace Service_Abstraction
{
    public interface IBasketService
    {
        Task<BasketDTo> GetBasketAsync(string Key);

        Task<BasketDTo> CreateOrUpdateBasketAsync(BasketDTo BasketDTo);

        Task<bool> DeleteBasketAsync(string Key);
    }
}
