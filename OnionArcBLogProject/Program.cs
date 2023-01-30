using Microsoft.EntityFrameworkCore;
using OnionArcBLogProject.Core.Service;
using OnionArcBLogProject.Entities.Context;
using OnionArcBLogProject.Service.Base;

using Microsoft.AspNetCore.Authentication.Cookies;

namespace OnionArcBLogProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddScoped(typeof(ICoreService<>), typeof(BaseService<>));//Icore service ile base service arasında bağlantı
            builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer("Server=tcp:blogdata.database.windows.net,1433;Initial Catalog=BlogDB;Persist Security Info=False;User ID=yunus;Password=Asdf_1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.LoginPath = "/Account/login"; //Yetki isteyen sayfalara girmek istediğiizde bizi yönlendireceği sayfayı belirle

                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}