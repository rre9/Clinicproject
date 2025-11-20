using ClinicProject.Models;
using Microsoft.AspNetCore.Identity;

namespace ClinicProject.Helpers {
    public static class AdminSeeder {

        public static async Task SeedAdminUser(WebApplication app) {
            // Create scope
            var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var adminEmail = "admin@clinic.com";
            var adminPassword = "Admin@123456";

            // Find admin user
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null) {
                // Create admin user
                admin = new AppUser {
                    Email = adminEmail,
                    UserName = adminEmail.Split("@")[0],
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);

                if (!result.Succeeded) {
                    throw new Exception("Can't create admin user");
                }

                // Assign admin role
                result = await userManager.AddToRoleAsync(admin, AppRoles.APP_ADMIN.ToString());
                if (!result.Succeeded) {
                    throw new Exception("Can't assign role to admin user");
                }
            }
        }

    }
}
