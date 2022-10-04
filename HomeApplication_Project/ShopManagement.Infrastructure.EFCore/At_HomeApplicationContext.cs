using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductCategoryAgg;
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


        public At_HomeApplicationContext(DbContextOptions<At_HomeApplicationContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProuctCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
