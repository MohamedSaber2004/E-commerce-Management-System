using Domain_Layer.Models.IdeneityModule;
using Domain_Layer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Presistence_Layer.Identity;
using System.Text.Json;

namespace Persistence_Layer
{
    public class DataSeeding(StoreDbContext dbContext,
                             UserManager<ApplicationUser> userManager,
                             RoleManager<IdentityRole> roleManager,
                             StoreIdentityDbContext identityDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                // check if building migrations is not migrated in database 
                if ((pendingMigrations).Any())
                {
                    await dbContext.Database.MigrateAsync();
                }

                if (!dbContext.ProductBrands.Any())
                {
                    //var ProductBrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence-Layer\Data\DataSeed\brands.json");
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Presistence-Layer\Data\DataSeed\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                       await dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }
                if (!dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Presistence-Layer\Data\DataSeed\products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                       await dbContext.Products.AddRangeAsync(Products);
                }
                if (!dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Presistence-Layer\Data\DataSeed\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                        await dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }
                if (!dbContext.Set<DeliveryMethod>().Any())
                {
                    var deliveryMethodData =  File.OpenRead(@"..\Infrastructure\Presistence-Layer\Data\DataSeed\delivery.json");
                    var deliveryMethod = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(deliveryMethodData);
                    if(deliveryMethod is not null && deliveryMethod.Any())
                        await dbContext.Set<DeliveryMethod>().AddRangeAsync(deliveryMethod);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "mmms77990@gmail.com",
                        DisplayName = "Mohamed Saber",
                        PhoneNumber = "01022812243",
                        UserName = "MohamedSaber107"
                    };

                    var User02 = new ApplicationUser()
                    {
                        Email = "mmms33041@gmail.com",
                        DisplayName = "Salma Mohamed",
                        PhoneNumber = "01155698835",
                        UserName = "Salma_Mohamed"
                    };

                    await userManager.CreateAsync(User01, "Mo#469200989");
                    await userManager.CreateAsync(User02, "Mo#469200989");

                    await userManager.AddToRoleAsync(User01, "SuperAdmin");
                    await userManager.AddToRoleAsync(User02, "Admin");
                }

            }
            catch (Exception ex)
            {

            }

            await identityDbContext.SaveChangesAsync();
        }
    }
}
