
using Shared.DataTransferObjects.OrderModuleDTos;

namespace Service_Abstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDTo> CreateOrder(OrderDTo order,string email);

        Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodsAsync();

        Task<IEnumerable<OrderToReturnDTo>> GetAllOrdersAsync(string email);

        Task<OrderToReturnDTo> GetOrderByIdAsync(Guid id);
    }
}
