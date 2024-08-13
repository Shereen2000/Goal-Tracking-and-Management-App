using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goal_Management_Web_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Goal_Management_Web_App.Data
{
    public static class SeedIdentityData
    {
        public const string Password = "XYZ123";

        private const string Email = "xyz@gmail.com";

        private const string Name = "xyz";

        private const string userRole= "User";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<AppDbContext>();

            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
 
            UserManager<User> userManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<UserManager<User>>();

            RoleManager<IdentityRole> roleManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

            if(await userManager.FindByNameAsync(Email) == null)
            {
                if(roleManager.FindByNameAsync(userRole) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(userRole));
                }

                User user = new User
                {
                    UserName = Email,
                    Email = Email,
                    Name = Name
                };

                IdentityResult result = await userManager.CreateAsync(user , Password );

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userRole);
                }
            }
        }
    }
}