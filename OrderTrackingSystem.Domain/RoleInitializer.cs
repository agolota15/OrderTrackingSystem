using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Domain
{
    public static class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string customerRole = "Customer";
            string sellerRole = "Seller";

            // Tworzenie ról
            if (await roleManager.FindByNameAsync(customerRole) == null)
                await roleManager.CreateAsync(new IdentityRole(customerRole));

            if (await roleManager.FindByNameAsync(sellerRole) == null)
                await roleManager.CreateAsync(new IdentityRole(sellerRole));

            // Tworzenie testowego sprzedawcy
            string sellerEmail = "seller@demo.pl";
            string sellerPassword = "Seller123!";
            var existingSeller = await userManager.FindByEmailAsync(sellerEmail);
            if (existingSeller == null)
            {
                var sellerUser = new IdentityUser
                {
                    Email = sellerEmail,
                    UserName = sellerEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(sellerUser, sellerPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(sellerUser, sellerRole);
                }
            }
        }
    }
}
