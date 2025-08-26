using AutoMapper;
using Domain_Layer.Exceptions;
using Domain_Layer.Models.OrderModule;
using Domain_Layer.Repository_Interfaces;
using Microsoft.Extensions.Configuration;
using Service_Abstraction;
using Shared.DataTransferObjects.BasketModuleDTos;
using Stripe;
using Product = Domain_Layer.Models.ProductModule.Product;

namespace Service_Implementation
{
    public class PaymentService(IConfiguration _configuration,
                                IBasketRepository _basketRepository,
                                IUnitOfWork _unitOfWork,
                                IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDTo> CreateOrUpdatePaymentIntent(string BasketId)
        {
            // Configure Stripe => [secret Key] : install package(Stripe.Net)
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            // Get basket by basket Id
            var Basket = await _basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundException(BasketId);

            // Get Amount - Get Product Price + Delivery Method Cost
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = Product.Price;
            }

            ArgumentNullException.ThrowIfNull(Basket.DeliveryMethodId);
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId.Value)
                             ?? throw new DeliveryNotFoundeException(Basket.DeliveryMethodId.Value);

            Basket.ShippingPrice = DeliveryMethod.Price;

            var BasketAmount = (long)(Basket.Items.Sum(item => item.Quantity * item.Price) + DeliveryMethod.Price) * 100;

            // Create Payment Intent [create - update]
            var PaymentService = new PaymentIntentService();
            if (Basket.PaymentIntentId is null) // create
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentService.CreateAsync(Options);
                Basket.PaymentIntentId = PaymentIntent.Id;
                Basket.ClientSecret = PaymentIntent.ClientSecret;
            }
            else // update
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount,
                };
                await PaymentService.UpdateAsync(Basket.PaymentIntentId, Options);
            }

            await _basketRepository.CreateOrUpdateBasketAsync(Basket);

            return _mapper.Map<BasketDTo>(Basket);
        }
    }
}
