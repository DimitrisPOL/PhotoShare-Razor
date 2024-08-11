using Microsoft.AspNetCore.Identity;
using PhotoShare.Infrastructure.Data.Users;
using System.Threading.Tasks;
namespace PhotoShare.Infrastructure.Data
{
    public class ApplicationDbContextDataSeed
    {
        /// <summary>
        ///     Seed users and roles in the Identity database.
        /// </summary>
        /// <param name="userManager">ASP.NET Core Identity User Manager</param>
        /// <param name="roleManager">ASP.NET Core Identity Role Manager</param>
        /// <returns></returns>
        public static async Task SeedAsync(UserManager<PhotographyUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {

                // Add roles supported
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
                await roleManager.CreateAsync(new IdentityRole("PHOTOGRAPHER"));

                // New admin user
                string adminUserName = "example@gmail.com";
                var adminUser = new PhotographyUser
                {
                    Email = "example@gmail.com",
                    City = "Athens",
                    Degree = "Masters",
                    Name = "admin",
                    ProfilePic = new byte[] { },
                    EmailConfirmed = true,
                    UserName = "example@gmail.com",
                    NormalizedUserName = "EXAMPLE@GMAIL.COM"
                };
                // Add new user and their role
                var res = await userManager.CreateAsync(adminUser, "Abc123!");
                adminUser = await userManager.FindByNameAsync(adminUserName);
                await userManager.AddToRoleAsync(adminUser, "ADMIN");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
