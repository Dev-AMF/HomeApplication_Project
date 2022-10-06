using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProuctMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            
            builder.HasKey(P => P.Id);

            builder.Property(P => P.Name).HasMaxLength(255).IsRequired();
            builder.Property(P => P.Code).HasMaxLength(15).IsRequired();
            builder.Property(P => P.ShortDescription).HasMaxLength(500);

            builder.HasOne(P => P.Category)
                   .WithMany(PC => PC.Products)
                   .HasForeignKey(P => P.CategoryId);

            builder.HasMany(P => P.ProductPicturesSlider)
                   .WithOne(PPS => PPS.Product)
                   .HasForeignKey(PPS => PPS.ProductId);
        }
    }
}
