using Domain_Layer.Repository_Interfaces;
using E_Commerce.Web.CustomMiddleWares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            // manual dependency injection 
            using var scope = app.Services.CreateScope(); 
            var ObjectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            
            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true
                };

                options.DocumentTitle = "My E-Commerce API";

                options.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                options.DocExpansion(DocExpansion.None);

                options.EnableFilter();

                // must add two services: AddSecurityDefinition - AddSecurityRequirement
                options.EnablePersistAuthorization();
            });

            return app;
        }

        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAuthentication(configurations =>
            {
                configurations.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configurations.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration.GetSection("JwtOptions")["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration.GetSection("JwtOptions")["Audience"],

                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"]!))
                };
            });

            return services;
        }
    }
}
