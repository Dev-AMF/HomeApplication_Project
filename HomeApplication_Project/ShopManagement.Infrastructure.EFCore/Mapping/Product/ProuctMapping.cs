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

            //-------------------- Product Picture --------------------//
            builder.OwnsOne(P => P.Picture,
                            Pp => 
                            {
                                Pp.ToTable("ProductsPictures");

                                Pp.HasKey(P => P.Id);

                                Pp.Property(Pp => Pp.Path).HasMaxLength(100);
                                Pp.Property(Pp => Pp.Alt).HasMaxLength(255);
                                Pp.Property(Pp => Pp.Title).HasMaxLength(500);

                                Pp.WithOwner(Pp => Pp.Product).HasForeignKey(Pp => Pp.ProductId);
                            });

            //-------------------- Product PageMeta --------------------//
            builder.OwnsOne(P => P.Metas,
                            Ppm =>
                            {
                                Ppm.ToTable("ProductsPageMetas");

                                Ppm.HasKey(P => P.Id);

                                Ppm.Property(Ppm => Ppm.Keywords).HasMaxLength(80);
                                Ppm.Property(Ppm => Ppm.MetaDescription).HasMaxLength(150);
                                Ppm.Property(Ppm => Ppm.Slug).HasMaxLength(300);

                                Ppm.WithOwner(Ppm => Ppm.Product).HasForeignKey(Ppm => Ppm.ProductId);
                            });


            builder.HasOne(P => P.Category)
                   .WithMany(PC => PC.Products)
                   .HasForeignKey(P => P.CategoryId);

            builder.HasMany(P => P.ProductPicturesSlider)
                   .WithOne(PPS => PPS.Product)
                   .HasForeignKey(PPS => PPS.ProductId);

            builder.HasMany(P => P.Comments)
                   .WithOne(C => C.Product)
                   .HasForeignKey(C => C.ProductId);
        }
    }
}
