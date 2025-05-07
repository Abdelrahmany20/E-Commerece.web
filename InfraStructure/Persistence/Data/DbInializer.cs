using Domain.Contracts;
using Domain.Models.Identity;
using Domain.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class DbInializer(StoreDBContext context,UserManager<ApplicationUser> userManger,RoleManager <IdentityRole> roleManager, StoreIdentityDbContext identityDbContext) : IDbInializer
    {
     

        public async Task InializeAsync()
        {


            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }



            try
            {
                if (!context.Set<ProductBrand>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"../InfraStructure/Persistence/Data/Seeds/brands.json");

                    var Objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductBrand>().AddRange(Objects);
                        await context.SaveChangesAsync();

                    }
                }
                if (!context.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"../InfraStructure/Persistence/Data/Seeds/types.json");

                    var Objects = JsonSerializer.Deserialize<List<ProductType>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductType>().AddRange(Objects);
                        await context.SaveChangesAsync();

                    }
                }
                if (!context.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"../InfraStructure/Persistence/Data/Seeds/products.json ");

                    var Objects = JsonSerializer.Deserialize<List<Product>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<Product>().AddRange(Objects);
                        await context.SaveChangesAsync();

                    }
                }



            }
            catch (Exception)
            {

                throw;
            }






        }


        public async Task IdentityInializeAsync()
        {

            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }


                if (!userManger.Users.Any())
                {

                    var User1 = new ApplicationUser()
                    {
                        Email = "Abdelrahman@gmail.com",
                        DisplayName = "Abdelrahman Yasser",
                        PhoneNumber = "01133423423",
                        UserName = "Abdo"
                    };


                    var User2 = new ApplicationUser()
                    {
                        Email = "ahmed@gmail.com",
                        DisplayName = "ahmed",
                        PhoneNumber = "01133423423",
                        UserName = "ahmed"


                    };

                    await userManger.CreateAsync(User1, "P@ssword1");
                    await userManger.CreateAsync(User2, "P@ssword1");

                    await userManger.AddToRoleAsync(User1, "Admin");
                    await userManger.AddToRoleAsync(User2, "SuperAdmin");


                }


                //await identityDbContext.SaveChangesAsync();


            }
            catch (Exception ex)
            {

                throw;
            }





        }


    }
}
