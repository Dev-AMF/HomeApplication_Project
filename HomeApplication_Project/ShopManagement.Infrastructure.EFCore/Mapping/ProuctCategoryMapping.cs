using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProuctCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            
            builder.HasKey(PC => PC.Id);

            builder.Property(PC => PC.Name).HasMaxLength(255).IsRequired() ;
            builder.Property(PC => PC.Description).HasMaxLength(500);   
        }
    }
}
