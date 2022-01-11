using EntityLayer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class DataContext : IdentityDbContext<UserAdmin>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-U1FS87R\SQLEXPRESS; Database=E-Shops; integrated security=true;").UseLazyLoadingProxies();
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }
      
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserAdmin> UserAdmins { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderImages> SliderImages { get; set; }
      
       

    }
}
