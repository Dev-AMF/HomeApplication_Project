using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class SlideMapping : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");
            builder.HasKey(S => S.Id);

            builder.Property(S => S.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(S => S.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(S => S.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(S => S.Heading).HasMaxLength(255).IsRequired();
            builder.Property(S => S.Title).HasMaxLength(255);
            builder.Property(S => S.Text).HasMaxLength(255);
            builder.Property(S => S.Text).HasMaxLength(50).IsRequired();
        }
    }
}
