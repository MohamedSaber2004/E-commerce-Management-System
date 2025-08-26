

using Domain_Layer.Models.OrderModule;

namespace Service_Implementation.Specifications
{
    class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId) : base(O => O.PaymentIntentId == PaymentIntentId)
        {
            
        }
    }
}
