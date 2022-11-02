using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountMangement.Infrastructure.EFCore.Mappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(R => R.Id);

            builder.Property(R => R.Name).HasMaxLength(100).IsRequired();

            builder.OwnsMany(R => R.Permissions, navigationBuilder =>
            {
                navigationBuilder.HasKey(P => P.Id);
                navigationBuilder.ToTable("RolePermissions");
                navigationBuilder.Ignore(x => x.Name);
                navigationBuilder.WithOwner(P => P.Role);
            });
        }
    }
}
