using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductsPictures");

            builder.HasKey(PP => PP.Id);

            builder.Property(PCP => PCP.Path).HasMaxLength(100);
            builder.Property(PCP => PCP.Alt).HasMaxLength(255);
            builder.Property(PCP => PCP.Title).HasMaxLength(500);

            builder.HasOne(PCP => PCP.Product)
                .WithOne(PC => PC.Picture)
                .HasForeignKey<Product>(P=>P.PictureId);
        }
    }
}
