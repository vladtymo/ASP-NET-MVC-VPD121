using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shop_MVC_VPD_121.Models;
using System;

namespace Shop_MVC_VPD_121.Helpers
{
    static class SeedExtensions
    {
        public static async Task SeedRoles(this IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdmin(this IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            const string USERNAME = "myadmin@myadmin.com";
            const string PASSWORD = "Admin1@";

            var existingUser = await userManager.FindByNameAsync(USERNAME);

            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = USERNAME,
                    Email = USERNAME,
                };

                var result = await userManager.CreateAsync(user, PASSWORD);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
