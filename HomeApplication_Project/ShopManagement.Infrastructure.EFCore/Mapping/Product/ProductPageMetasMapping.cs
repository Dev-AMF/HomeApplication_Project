using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductPageMetasMapping : IEntityTypeConfiguration<ProductPageMetas>
    {
        public void Configure(EntityTypeBuilder<ProductPageMetas> builder)
        {
            builder.ToTable("ProductsPageMetas");
            
            builder.HasKey(PCPM => PCPM.Id);

            builder.Property(PPM => PPM.Keywords).HasMaxLength(80);
            builder.Property(PPM => PPM.MetaDescription).HasMaxLength(150);
            builder.Property(PPM => PPM.Slug).HasMaxLength(300);

            builder.HasOne(PPM => PPM.Product)
                .WithOne(PC => PC.Metas)
                .HasForeignKey<Product>(PC => PC.MetasId);
        }
    }
}
