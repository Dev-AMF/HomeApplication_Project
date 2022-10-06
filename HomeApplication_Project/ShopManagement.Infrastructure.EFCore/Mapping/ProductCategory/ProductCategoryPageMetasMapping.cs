using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductCategoryPageMetasMapping : IEntityTypeConfiguration<ProductCategoryPageMetas>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryPageMetas> builder)
        {
            builder.ToTable("ProductCategoriesPageMetas");
            
            builder.HasKey(PCPM => PCPM.Id);

            builder.Property(PCPM => PCPM.Keywords).HasMaxLength(80);
            builder.Property(PCPM => PCPM.MetaDescription).HasMaxLength(150);
            builder.Property(PCPM => PCPM.Slug).HasMaxLength(300);

            builder.HasOne(PCPM => PCPM.ProductCategory)
                .WithOne(PC => PC.Metas)
                .HasForeignKey<ProductCategory>(PC => PC.MetasId);
        }
    }
}
