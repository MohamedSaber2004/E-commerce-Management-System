using AutoMapper;
using Domain_Layer.Models.IdeneityModule;
using Domain_Layer.Repository_Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service_Abstraction;

namespace Service_Implementation
{
    public class ServiceManager(IUnitOfWork unitOfWork,
                                IMapper mapper, 
                                IBasketRepository _basketRepository,
                                UserManager<ApplicationUser> _userManager,
                                IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(new ProductService(unitOfWork, mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(new BasketService(_basketRepository, mapper));
        private readonly Lazy<IAuthenticationService> _authenticationService = new Lazy<IAuthenticationService>(new AuthenticationService(_userManager,_configuration,mapper));
        private readonly Lazy<IOrderService> _orderService = new Lazy<IOrderService>(new OrderService(mapper,_basketRepository,unitOfWork));
        private readonly Lazy<IPaymentService> _paymentService = new Lazy<IPaymentService>(new PaymentService(_configuration,_basketRepository,unitOfWork,mapper));

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService => _orderService.Value;

        public IPaymentService PaymentsService => _paymentService.Value;
    }
}
