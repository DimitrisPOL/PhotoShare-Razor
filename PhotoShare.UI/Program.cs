using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Extensions;
using PhotoShare.Infrastructure.Configuration;
using PhotoShare.Infrastructure.Data.Users;
using PhotoShare.Infrastructure.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PhotoShare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<ApplicationConfiguration>(builder.Configuration);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(connectionString, b => b.MigrationsAssembly("PhotoShare.Infrastructure")); });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<PhotographyUser>(options => options.SignIn.RequireConfirmedAccount = true)
                          .AddRoles<IdentityRole>()
                          .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<IBlobStorageManager, BlobStorageManager>();
            builder.Services.AddSingleton<IEmaiSender, EmailSender>();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.EnsureIdentityDbIsCreated();
            //app.SeedIdentityDataAsync();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
