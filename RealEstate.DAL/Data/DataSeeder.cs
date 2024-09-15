using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.DAL.Enums;

namespace RealEstate.PL.Helper
{
    public class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                string roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var defaultUserEmail = "superadmin@gmail.com";
            var defaultUser = await userManager.FindByEmailAsync(defaultUserEmail);

            if (defaultUser == null)
            {
                defaultUser = new IdentityUser
                {
                    UserName = defaultUserEmail,
                    Email = defaultUserEmail
                };

                var result = await userManager.CreateAsync(defaultUser, "SuperAdmin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, UserRole.SuperAdmin.ToString());
                }
            }
        }
    }
}
