using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FeelAtHomeRestaurant.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FeelAtHomeRestaurant.Service.Interface;
using FeelAtHomeRestaurant.Rpository.Interface;
using FeelAtHomeRestaurant.Rpository;
using FeelAtHomeRestaurant.AutoMappers;
using AutoMapper;
using FeelAtHomeRestaurant.Service;
using Microsoft.Extensions.FileProviders;
using System.IO;
using FeelAtHomeRestaurant.Utility;

namespace FeelAtHomeRestaurant
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); 
            //services.AddIdentity<ApplicationUser, IdentityRole>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Account/Manage");
                options.Conventions.AuthorizePage("/Account/Logout");
                options.Conventions.AuthorizePage("/Details");
                options.Conventions.AuthorizeFolder("/Controllers/Shoppingcart/Create");
            });
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SD.AdminEndUser, policy => policy.RequireRole(SD.AdminEndUser));
            });
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "2436226963304405";
                facebookOptions.AppSecret = "09c168d208290d230015643a474c7c67";
            });
            services.AddScoped<ICategoryTypeService, CategoryTypeService> ();
            services.AddScoped<ICategoryTypeRepo, CategoryTypeRepository>();
            services.AddScoped<IFoodTypeService, FoodTypeService>();
            services.AddScoped<IFoodTypeRepo, FoodTypeRepo>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IMenuItemRepo, MenuItemRepo>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<ICartDetailRepository, CartDetailRepository>();
            services.AddScoped<ICartDetailService, CartDetailService>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IEmailSenderRepository, EmailSenderRepository>();
            Mapper.Initialize(
               cfg => {
                   cfg.AddProfile<MainMapper>();
               });

            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MenuItemImages"))
                );
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProfilePic"))
                );
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseSession();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
