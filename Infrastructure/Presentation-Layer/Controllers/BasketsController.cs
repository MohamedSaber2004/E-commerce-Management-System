
using Microsoft.AspNetCore.Mvc;
using Service_Abstraction;
using Shared.DataTransferObjects.BasketModuleDTos;

namespace Presentation_Layer.Controllers
{
    public class BasketsController(IServiceManager _serviceManager) : ApiController
    {
        // Get Basket
        [HttpGet] 
        public async Task<ActionResult<BasketDTo>> GetBasket(string Key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(Key);
            return Ok(Basket);
        }

        // Create Or Update Basket
        [HttpPost]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdateBasket(BasketDTo Basket)
        {
            var basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(Basket);
            return Ok(basket);
        }

        // Delete Basket
        [HttpDelete("{Key}")] // DELETE BaseURL/api/Basket/Key
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
