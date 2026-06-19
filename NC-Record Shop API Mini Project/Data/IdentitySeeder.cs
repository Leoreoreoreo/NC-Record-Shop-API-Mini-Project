using Microsoft.AspNetCore.Identity;

namespace NC_Record_Shop_API_Mini_Project.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider services, IConfiguration config)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var email = config["AdminUser:Email"] ?? "admin@recordshop.com";
            var password = config["AdminUser:Password"] ?? "Admin123!";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
