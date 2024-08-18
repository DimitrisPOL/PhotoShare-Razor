using Microsoft.AspNetCore.Identity;
using PhotoShare.Infrastructure.Data.Users;
using System.Threading.Tasks;
namespace PhotoShare.Infrastructure.Data
{
    public class ApplicationDbContextDataSeed
    {
        public static async Task SeedAsync(UserManager<PhotographyUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var adminRole = new IdentityRole("admin");
                
            if((await roleManager.GetRoleNameAsync(adminRole)) == null)
            {

                adminRole.NormalizedName = "ADMIN";
                adminRole.Id = Guid.NewGuid().ToString();
                adminRole.ConcurrencyStamp = adminRole.Id;

                await roleManager.CreateAsync(adminRole);
            }


            var photographerRole = new IdentityRole("photographer");

            if ((await roleManager.GetRoleNameAsync(photographerRole)) == null)
            {
                photographerRole.NormalizedName = "PHOTOGRAPHER";
                photographerRole.Id = Guid.NewGuid().ToString();
                photographerRole.ConcurrencyStamp = photographerRole.Id;

                await roleManager.CreateAsync(photographerRole);
            }

            string adminUserName = "admin@photoshare.com";

            PhotographyUser adminUser = await userManager.FindByEmailAsync(adminUserName);



            if (adminUser == null) 
            {
                adminUser = new PhotographyUser
                {
                    Email = adminUserName,
                    City = "Athens",
                    Degree = "Masters",
                    Name = "admin",
                    ProfilePic = new byte[] { },
                    EmailConfirmed = true,
                    UserName = adminUserName,
                    NormalizedUserName = "ADMIN@PHOTOSHARE.COM"
                };

                // Add new user and their role
                await userManager.CreateAsync(adminUser, "Abc123!");

                await userManager.AddToRoleAsync(adminUser, "ADMIN");
            }
        }
    }
}
