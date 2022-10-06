using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductCategoryPictureMapping : IEntityTypeConfiguration<ProductCategoryPicture>
    {
        public void Configure(EntityTypeBuilder<ProductCategoryPicture> builder)
        {
            builder.ToTable("ProductCategoriesPictures");

            builder.HasKey(PCP => PCP.Id);

            builder.Property(PCP => PCP.Path).HasMaxLength(100);
            builder.Property(PCP => PCP.Alt).HasMaxLength(255);
            builder.Property(PCP => PCP.Title).HasMaxLength(500);

            builder.HasOne(PCP => PCP.ProductCategory)
                .WithOne(PC => PC.Picture)
                .HasForeignKey<ProductCategory>(PC=>PC.PictureId);
        }
    }
}
