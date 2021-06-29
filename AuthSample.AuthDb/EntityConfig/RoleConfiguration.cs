using System;
using System.Collections.Generic;
using System.Text;
using AuthSample.AuthDb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthSample.AuthDb.EntityConfig
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.HasMany(_ => _.RolePermissions)
                .WithOne(_ => _.Role)
                .HasForeignKey(_ => _.RoleId);

            builder.Property(_ => _.RoleType).HasConversion<int>();
        }
    }
}
