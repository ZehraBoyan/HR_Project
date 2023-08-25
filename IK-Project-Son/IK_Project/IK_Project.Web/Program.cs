using IK_Project.Core.DTOs;
using IK_Project.Core.Repositories;
using IK_Project.Repository.Context;
using IK_Project.Repository.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IK_Project.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			//builder.Services.AddDbContext<AppDbContext>(option =>
			//{
			//	option.UseSqlServer(builder.Configuration.GetConnectionString("Con"));
			//});
			// Add services to the container.
			builder.Services.AddControllersWithViews();

            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromDays(1); 

            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
			{
				options.LoginPath = "/Account/Login";
				options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
				options.SlidingExpiration = true;
				options.AccessDeniedPath = "/Forbidden/";
			});

            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("Con"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
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

			app.UseAuthorization();
            app.UseSession();//yukarda eklenmis olan sessionu kullanmak icin eklememiz gereken satirdir.
            app.MapControllerRoute(
				name: "areas",
				pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				 );
            app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=Login}/{id?}");

			app.Run();
		}
	}
}