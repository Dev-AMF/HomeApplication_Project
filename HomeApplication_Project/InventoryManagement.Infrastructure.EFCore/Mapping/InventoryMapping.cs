using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Infrastructure.EFCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(I=> I.Id);

            builder.OwnsMany(I => I.Operations, IOp =>
            {
                IOp.HasKey(I => I.Id);
                IOp.ToTable("InventoryOperations");
                IOp.Property(I => I.Description).HasMaxLength(1000);
                IOp.WithOwner(I => I.Inventory).HasForeignKey(I => I.InventoryId);
            });
        }
    }
}
