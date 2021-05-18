using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Store.DataAcess.Entities;
using Store.DataAcess.Entities.Enums;
using System;
using System.Threading.Tasks;

namespace Store.DataAcess.Initialization
{
    public static class DataBaseInitialization
    {
        public static async Task InitializeAsync(this IServiceCollection services)
        {
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<StoreUser>>();
            var roleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole<long>>>();

            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            string firstName = "Aleksandr";
            string lastName = "Nesheretnuy";


            try
            {
                var role = await roleManager.FindByNameAsync(UserRole.Admin.ToString().ToLower());
                if (role is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(UserRole.Admin.ToString()));
                }
                role = await roleManager.FindByNameAsync(UserRole.Client.ToString().ToLower());
                if (role is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(UserRole.Client.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                StoreUser admin = new StoreUser { Email = adminEmail, UserName = adminEmail, FirstName = firstName, LastName = lastName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
