using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Domain.Values;
using PhotoShare.Infrastructure.Data.Users;
using System.Reflection.Emit;

namespace PhotoShare.Data
{
    public class ApplicationDbContext : IdentityDbContext<PhotographyUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Photographer> Photographers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "2652f744-d840-4c18-9ecf-87e84355101f";

           // seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //create user
            var appUser = new PhotographyUser
            {
                Id = ADMIN_ID,
                Email = "example@gmail.com",
                City = "Athens",
                Degree = "Masters",
                Name = "admin",
                ProfilePic = new byte[] {},
                EmailConfirmed = true,
                UserName = "example@gmail.com",
             NormalizedUserName = "EXAMPLE@GMAIL.COM"
            };

            //set user password
            PasswordHasher<PhotographyUser> ph = new PasswordHasher<PhotographyUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "admin");

            //seed user
            builder.Entity<PhotographyUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });


            base.OnModelCreating(builder);
        }
    }
}

