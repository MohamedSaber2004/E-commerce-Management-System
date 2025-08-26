using AutoMapper;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.OrderModule;
using Domain_Layer.Models.ProductModule;
using Domain_Layer.Repository_Interfaces;
using Service_Abstraction;
using Service_Implementation.Specifications;
using Service_Implementation.Specifications.OrderModuleSpecifications;
using Shared.DataTransferObjects.AuthenticationModuleDTos;
using Shared.DataTransferObjects.OrderModuleDTos;

namespace Service_Implementation
{
    public class OrderService(IMapper _mapper,
                              IBasketRepository _basketRepository,
                              IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDTo> CreateOrder(OrderDTo orderDTo, string email)
        {
            // Map Address To Order Address
            var OrderAddress = _mapper.Map<AddressDTo, OrderAddress>(orderDTo.ShipToAddress);

            // Get Basket 
            var Basket = await _basketRepository.GetBasketAsync(orderDTo.BasketId) 
                            ?? throw new BasketNotFoundException(orderDTo.BasketId);

            ArgumentNullException.ThrowIfNullOrEmpty(Basket.PaymentIntentId);

            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var OrderSpec = new OrderWithPaymentIntentIdSpecifications(Basket.PaymentIntentId);
            var ExistingOrder = await OrderRepo.GetByIdAsync(OrderSpec);
            if (ExistingOrder is not null) OrderRepo.Remove(ExistingOrder);

            // Create OrderItem List
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                                ?? throw new ProductNotFoundException(item.Id);

                OrderItems.Add(CreateOrderItem(item, Product));
            }

            // Get Delivery Method
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTo.DeliveryMethodId)
                                   ?? throw new DeliveryNotFoundeException(orderDTo.DeliveryMethodId);

            // Calculate Sub Total
            var SubTotal = OrderItems.Sum(OI => OI.Price * OI.Quantity);

            var Order = new Order(email, OrderAddress, DeliveryMethod, OrderItems, SubTotal, Basket.PaymentIntentId);
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order,OrderToReturnDTo>(Order);
        }

        private static OrderItem CreateOrderItem(Domain_Layer.Models.BasketModule.BasketItem item, Product Product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered()
                {
                    ProductId = Product.Id,
                    PictureUrl = Product.PictureUrl,
                    ProductName = Product.Name
                },
                Price = Product.Price,
                Quantity = item.Quantity,
            };
        }

        public async Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>,IEnumerable<DeliveryMethodDTo>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDTo>> GetAllOrdersAsync(string email)
        {
            var OrderSpecifications = new OrderSpecifications(email);
            var Orders = await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(OrderSpecifications);
            return _mapper.Map<IEnumerable<Order>,IEnumerable<OrderToReturnDTo>>(Orders);
        }

        public async Task<OrderToReturnDTo> GetOrderByIdAsync(Guid id)
        {
            var OrderSpecifications = new OrderSpecifications(id);
            var Order= await _unitOfWork.GetRepository<Order,Guid>().GetByIdAsync(OrderSpecifications);
            return _mapper.Map<Order, OrderToReturnDTo>(Order);
        }
    }
}
