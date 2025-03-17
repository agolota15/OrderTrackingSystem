using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web
{
    public static class RoleInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Admin", "Customer", "Seller" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
<<<<<<< HEAD
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Konto administratora
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123"; // Pamiętaj, by w produkcji używać silnych haseł
=======
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Utwórz konto administratora
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123"; // W celach demonstracyjnych – pamiętaj o silnym haśle w produkcji
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
<<<<<<< HEAD
                    await userManager.AddToRoleAsync(adminUser, "Admin");
=======
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            }
        }
    }
}
