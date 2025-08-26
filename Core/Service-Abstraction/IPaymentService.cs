using Shared.DataTransferObjects.BasketModuleDTos;

namespace Service_Abstraction
{
    public interface IPaymentService
    {
        Task<BasketDTo> CreateOrUpdatePaymentIntent(string BasketId);
    }
}
