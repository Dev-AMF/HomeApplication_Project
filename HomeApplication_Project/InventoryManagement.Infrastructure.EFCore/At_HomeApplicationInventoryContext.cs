using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Infrastructure.EFCore
{
    public class At_HomeApplicationInventoryContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }

        public At_HomeApplicationInventoryContext(DbContextOptions<At_HomeApplicationInventoryContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder); 
        }
    }   
}
