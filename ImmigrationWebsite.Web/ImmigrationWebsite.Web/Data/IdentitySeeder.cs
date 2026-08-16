using Microsoft.AspNetCore.Identity;

namespace ImmigrationWebsite.Web.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        var userManager = serviceProvider
            .GetRequiredService<UserManager<IdentityUser>>();

        const string roleName = "Admin";
        const string adminEmail = "admin@immigrationwebsite.com";
        const string adminPassword = "Admin@123456";

        // Create Admin role
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(
                new IdentityRole(roleName));
        }

        // Find admin user
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        // Create admin user
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(
                adminUser,
                adminPassword);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(
                        ", ",
                        result.Errors.Select(x => x.Description)));
            }
        }

        // Add Admin role
        if (!await userManager.IsInRoleAsync(adminUser, roleName))
        {
            await userManager.AddToRoleAsync(
                adminUser,
                roleName);
        }
    }
}