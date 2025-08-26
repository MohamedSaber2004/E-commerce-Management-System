
using AutoMapper;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.BasketModule;
using Domain_Layer.Repository_Interfaces;
using Service_Abstraction;
using Shared.DataTransferObjects.BasketModuleDTos;

namespace Service_Implementation
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTo> CreateOrUpdateBasketAsync(BasketDTo BasketDTo)
        {
            var CustomerBasket = _mapper.Map<BasketDTo, CustomerBasket>(BasketDTo);
            var isCreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (isCreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(BasketDTo.Id);
            else
                throw new Exception("Can Not Update Or Create Basket Now, Try Again Later");
        }


        public async Task<BasketDTo> GetBasketAsync(string Key)
        {
            var Basket = await _basketRepository.GetBasketAsync(Key);
            if(Basket is not null)
            {
                return _mapper.Map<CustomerBasket, BasketDTo>(Basket);
            }
            else
            {
                throw new BasketNotFoundException(Key);
            }
        }
        public async Task<bool> DeleteBasketAsync(string Key)
        => await _basketRepository.DeleteBasketAsync(Key);
    }
}
