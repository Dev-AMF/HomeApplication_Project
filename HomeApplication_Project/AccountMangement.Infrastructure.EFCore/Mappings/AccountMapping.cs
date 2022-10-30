using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountMangement.Infrastructure.EFCore.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(AT => AT.Id);

            builder.Property(AT => AT.Fullname).HasMaxLength(100).IsRequired();
            builder.Property(AT => AT.Username).HasMaxLength(100).IsRequired();
            builder.Property(AT => AT.Password).HasMaxLength(1000).IsRequired();
            builder.Property(AT => AT.ProfilePhoto).HasMaxLength(500).IsRequired();
            builder.Property(AT => AT.MobileNo).HasMaxLength(20).IsRequired();

            builder.HasOne(AT => AT.Role)
                   .WithMany(R => R.Accounts)
                   .HasForeignKey(AT => AT.RoleId);
        }
    }
}
