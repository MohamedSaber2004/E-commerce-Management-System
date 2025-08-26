
using Domain_Layer.Repository_Interfaces;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence_Layer;
using Persistence_Layer.Data;
using Presistence_Layer;
using Service_Implementation;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            builder.Services.AddSwaggerServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.AddWebApplicationServices();

            builder.Services.AddJwtService(builder.Configuration);

            #endregion

            var app = builder.Build();

            await app.SeedDataBaseAsync();

            #region Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
