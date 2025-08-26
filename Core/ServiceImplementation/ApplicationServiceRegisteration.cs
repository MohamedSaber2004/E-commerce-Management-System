using Microsoft.Extensions.DependencyInjection;
using Service_Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Implementation
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssemblyReference).Assembly);
            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>());

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>());

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>());

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(provider =>
            () => provider.GetRequiredService<IOrderService>());

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<Func<IPaymentService>>(provider => 
            () => provider.GetRequiredService<IPaymentService>());

            services.AddScoped<ICacheService, CacheService>();


            return services;
        }
    }
}
