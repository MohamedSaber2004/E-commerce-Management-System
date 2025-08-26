using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_Abstraction;
using Shared.DataTransferObjects.BasketModuleDTos;

namespace Presentation_Layer.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager):ApiController
    {
        // Create Or Update Payment Intent Id
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDTo>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var basket =  await _serviceManager.PaymentsService.CreateOrUpdatePaymentIntent(BasketId);
            return Ok(basket);
        }
    }
}
