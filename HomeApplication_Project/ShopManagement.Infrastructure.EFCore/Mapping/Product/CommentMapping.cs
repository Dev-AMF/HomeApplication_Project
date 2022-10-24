using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(C => C.Id);

            builder.Property(C => C.Name).HasMaxLength(500);
            builder.Property(C => C.Email).HasMaxLength(500);
            builder.Property(C => C.Message).HasMaxLength(1000);

            builder.HasOne(C => C.Product)
                .WithMany(P => P.Comments)
                .HasForeignKey(C => C.ProductId);
        }
    }
}
