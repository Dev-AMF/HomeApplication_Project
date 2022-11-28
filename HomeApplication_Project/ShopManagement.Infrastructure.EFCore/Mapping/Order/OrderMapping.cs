using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(O => O.Id);
            builder.Property(O => O.IssueTrackingNo).HasMaxLength(8);

            builder.OwnsMany(O => O.Items, navigationBuilder =>
            {
                navigationBuilder.ToTable("OrderItems");
                navigationBuilder.HasKey(OI => OI.Id);
                
                navigationBuilder.WithOwner(OI => OI.Order)
                                  .HasForeignKey(OI => OI.OrderId);
            });
        }
    }
}
