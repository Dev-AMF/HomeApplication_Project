using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureSliderAgg;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.EFCore.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore
{
    public class At_HomeApplicationContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryPageMetas> ProductCategoriesPageMetas { get; set; }
        public DbSet<ProductCategoryPicture> ProductCategoriesPictures { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPageMetas> ProductsPageMetas { get; set; }
        public DbSet<ProductPicture> ProductsPictures { get; set; }

        
        public DbSet<ProductPictureSlider> ProductsPicturesSliders { get; set; }
        public DbSet<Slide> Sliders { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        
        public At_HomeApplicationContext(DbContextOptions<At_HomeApplicationContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProuctMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
