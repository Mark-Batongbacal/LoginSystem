using LoginSystem.Models.Database;
using LoginSystem.Repository.Users;
using LoginSystem.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LoginSystem
{
    public class Program
    {
        //Scaffold-DbContext "Server=EA611-07; Database=LoginSystem; User ID=login_system; password=12345678; TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models/Database 
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LoginSystemContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
            });

            builder.Services.AddScoped<IUserDataManager, UserDataManager>();
            builder.Services.AddScoped<IUserManager, UserManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
