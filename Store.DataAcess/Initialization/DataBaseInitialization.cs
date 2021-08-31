using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Store.DataAcess.Entities;
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
            string fullName = "Aleksandr Nesheretnuy";

            try
            {
                var role = await roleManager.FindByNameAsync(UserRoleType.Admin.ToString().ToLower());
                if (role is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(UserRoleType.Admin.ToString()));
                }
                role = await roleManager.FindByNameAsync(UserRoleType.Client.ToString().ToLower());
                if (role is null)
                {
                    await roleManager.CreateAsync(new IdentityRole<long>(UserRoleType.Client.ToString()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (await userManager.FindByEmailAsync(adminEmail) is null)
            {
                StoreUser admin = new StoreUser { Email = adminEmail, UserName = adminEmail, FirstName = firstName, LastName = lastName, FullName = fullName };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
