using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommnetManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(C => C.Id);

            builder.Property(C => C.Name).HasMaxLength(500);
            builder.Property(C => C.Email).HasMaxLength(500);
            builder.Property(C => C.Website).HasMaxLength(500);
            builder.Property(C => C.Message).HasMaxLength(1000);
        }
    }
}
