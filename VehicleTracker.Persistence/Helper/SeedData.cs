using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTracker.Core.Enums;
using VehicleTracker.Infrastructure.Identity;

namespace VehicleTracker.Persistence.Helper
{
    public class SeedData
    {
        public static async Task SeedRoleData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            if(!await roleManager.RoleExistsAsync(Roles.Administrator.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
            if (!await roleManager.RoleExistsAsync(Roles.User.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            //Seed Default User
            if (await userManager.Users.AnyAsync(u => u.Email.ToLower() == "admin@admin.com")) { }
            else
            {
                var defaultUser = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
                await userManager.CreateAsync(defaultUser, "1234");
                await userManager.AddToRoleAsync(defaultUser, Roles.Administrator.ToString());
            }
        }
    }
}
