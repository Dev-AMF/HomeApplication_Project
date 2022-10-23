using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            builder.HasKey(PC => PC.Id);

            builder.Property(PC => PC.Name).HasMaxLength(255).IsRequired();
            builder.Property(PC => PC.Description).HasMaxLength(500);


            //-------------------- Category Picture --------------------//
            builder.OwnsOne(PC => PC.Picture,
                            PCp =>
                            {
                                PCp.ToTable("ProductCategoriesPictures");

                                PCp.HasKey(PCp => PCp.Id);

                                PCp.Property(PCp => PCp.Path).HasMaxLength(100);
                                PCp.Property(PCp => PCp.Alt).HasMaxLength(255);
                                PCp.Property(PCp => PCp.Title).HasMaxLength(500);

                                PCp.WithOwner(PCp => PCp.ProductCategory).HasForeignKey(PCp => PCp.ProductCategoryId);
                            });

            //-------------------- Category PageMeta --------------------//
            builder.OwnsOne(PC => PC.Metas,
                            PCm =>
                            {
                                PCm.ToTable("ProductCategoriesPageMetas");

                                PCm.HasKey(PCm => PCm.Id);

                                PCm.Property(PCm => PCm.Keywords).HasMaxLength(80);
                                PCm.Property(PCm => PCm.MetaDescription).HasMaxLength(150);
                                PCm.Property(PCm => PCm.Slug).HasMaxLength(300);

                            });




            builder.HasMany(PC => PC.Products)
                   .WithOne(P => P.Category)
                   .HasForeignKey(P => P.CategoryId);

        }
    }
}
