using System;
using System.Collections.Generic;
using System.Text;
using FeelAtHomeRestaurant.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FeelAtHomeRestaurant.ViewModel;
using Microsoft.AspNetCore.Http;

namespace FeelAtHomeRestaurant.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { //private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        
        public DbSet<CategoryType> CategoryTypes { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<EmailCredentials> EmailCredentials { get; set; }
        public DbSet<ImageSlider> ImageSliders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=FeelAtHomeRestaurant;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

       
    }
}
