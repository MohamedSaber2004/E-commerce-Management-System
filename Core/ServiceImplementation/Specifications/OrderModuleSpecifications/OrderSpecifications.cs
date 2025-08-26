using Domain_Layer.Models.OrderModule;

namespace Service_Implementation.Specifications.OrderModuleSpecifications
{
    class OrderSpecifications:BaseSpecifications<Order,Guid> 
    {
        public OrderSpecifications(string Email):base(O => O.BuyerEmail == Email)
        {
            IncludesEagerLoading();
            AddOrderByDesc(O => O.OrderDate);
        }

        public OrderSpecifications(Guid id):base(O => O.Id == id) 
        {
            IncludesEagerLoading();
        }

        private void IncludesEagerLoading()
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
        }
    }
}
