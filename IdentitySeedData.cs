using BusStation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusStation
{
    public class IdentitySeedData
    {
        private const string adminEmail = "admin@gmail.com";
        private const string adminPassword = "Qwerty123!";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {

            ApplicationDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<ApplicationDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                var admin = new IdentityUser { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
            }
        }
    }
}
