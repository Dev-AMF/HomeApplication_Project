using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureSliderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductPictureSliderMapping : IEntityTypeConfiguration<ProductPictureSlider>
    {
        public void Configure(EntityTypeBuilder<ProductPictureSlider> builder)
        {
            builder.ToTable("ProductPicturesSlider");

            builder.HasKey(PPS => PPS.Id);

            builder.Property(PPS => PPS.Path).HasMaxLength(1000).IsRequired();
            builder.Property(PPS => PPS.Alt).HasMaxLength(500).IsRequired();
            builder.Property(PPS => PPS.CreationDate).HasMaxLength(500).IsRequired();

            builder.HasOne(PPS => PPS.Product)
                .WithMany(P => P.ProductPicturesSlider)
                .HasForeignKey(PPS => PPS.ProductId);
        }
    }
}
