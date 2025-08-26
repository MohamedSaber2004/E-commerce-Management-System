using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service_Abstraction;
using Shared.DataTransferObjects.OrderModuleDTos;

namespace Presentation_Layer.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager):ApiController
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDTo>> CreateOrder(OrderDTo orderDTo)
        {
            var Order = await _serviceManager.OrderService.CreateOrder(orderDTo, GetEmailFromToken());
            return Ok(Order);
        }

        // Get Delivery Methods
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTo>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }

        // Get All Order By Email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDTo>>> GetAllOrders()
        {
            var Orders = await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);
        }

        // Get Order By Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDTo>> GetOrderById(Guid id)
        {
            var Order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Order);
        }
    }
}
