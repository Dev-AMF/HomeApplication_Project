using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EFCore.Mappings
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(A => A.Id);

            builder.Property(A => A.Title).HasMaxLength(500);
            builder.Property(A => A.ShortDescription).HasMaxLength(1000);
            builder.Property(A => A.Picture).HasMaxLength(500);
            builder.Property(A => A.PictureAlt).HasMaxLength(500);
            builder.Property(A => A.PictureTitle).HasMaxLength(500);
            builder.Property(A => A.Slug).HasMaxLength(600);
            builder.Property(A => A.Keywords).HasMaxLength(100);
            builder.Property(A => A.MetaDescription).HasMaxLength(150);
            builder.Property(A => A.CanonicalAddress).HasMaxLength(1000);

            builder.HasOne(A => A.Category)
                   .WithMany(AC => AC.Articles)
                   .HasForeignKey(A => A.CategoryId);
        }
    }
}
