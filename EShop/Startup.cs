using DataAccessLayer.Data;
using EntityLayer.Entity;
using EShop.Areas.Admin.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddIdentity<UserAdmin, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opt =>
            {
                //opt.Lockout.AllowedForNewUsers = true;
                //opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                //opt.Lockout.MaxFailedAccessAttempts = 5; //max deneme sayýsý
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;


            });

            services.ConfigureApplicationCookie
              (
              opt =>
              {
                  opt.ExpireTimeSpan = System.TimeSpan.FromDays(60);
                  opt.LoginPath = opt.LoginPath = "/Admin/Admin/Login/"; opt.AccessDeniedPath = "/Account/AccessDenied/"; opt.LogoutPath = "/Account/LogOut/"; opt.ExpireTimeSpan = TimeSpan.FromMinutes(6);
                  opt.Cookie = new CookieBuilder
                  {

                      HttpOnly = true,
                      SecurePolicy = CookieSecurePolicy.SameAsRequest,


                  };
              });
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<UserAdmin> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                );
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedIdentity.Seed3(userManager, roleManager);
        }
    }
}
