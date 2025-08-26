
using Domain_Layer.Models.IdeneityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Presistence_Layer.Identity;
using StackExchange.Redis;

namespace Presistence_Layer
{
    public static class InfrastructureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                //Options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddDbContext<StoreIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddScoped<IDataSeeding, DataSeeding>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString"));
            });


            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
    }
}
