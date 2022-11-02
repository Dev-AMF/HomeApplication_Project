using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountMangement.Infrastructure.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;
using System;

namespace AccountMangement.Infrastructure
{
    public class At_HomeApplicationAccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public At_HomeApplicationAccountContext(DbContextOptions<At_HomeApplicationAccountContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AccountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
