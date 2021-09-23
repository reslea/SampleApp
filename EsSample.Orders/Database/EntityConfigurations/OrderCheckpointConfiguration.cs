using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsSample.Orders.Database.EntityConfigurations
{
    public class OrderCheckpointConfiguration : IEntityTypeConfiguration<OrderCheckpoint>
    {
        public void Configure(EntityTypeBuilder<OrderCheckpoint> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.HasIndex(o => o.OrderId).IsUnique();

            builder.HasOne(o => o.Order)
                .WithOne()
                .HasForeignKey<OrderCheckpoint>(o => o.OrderId);
        }
    }
}
