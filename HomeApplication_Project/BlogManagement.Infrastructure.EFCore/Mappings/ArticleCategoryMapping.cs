using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mappings
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");
            builder.HasKey(AC => AC.Id);

            builder.Property(AC => AC.Name).HasMaxLength(500);
            builder.Property(AC => AC.Description).HasMaxLength(2000);
            builder.Property(AC => AC.Picture).HasMaxLength(500);
            builder.Property(AC => AC.PictureAlt).HasMaxLength(500);
            builder.Property(AC => AC.PictureTitle).HasMaxLength(500);
            builder.Property(AC => AC.Slug).HasMaxLength(600);
            builder.Property(AC => AC.Keywords).HasMaxLength(100);
            builder.Property(AC => AC.MetaDescription).HasMaxLength(150);
            builder.Property(AC => AC.CanonicalAddress).HasMaxLength(1000);

            builder.HasMany(AC => AC.Articles)
                   .WithOne(A => A.Category)
                   .HasForeignKey(A => A.CategoryId);
        }
    }
}
