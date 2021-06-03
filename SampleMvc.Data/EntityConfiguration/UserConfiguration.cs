using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMvc.Data.Entity;

namespace SampleMvc.Data.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.Property(u => u.Phone).IsRequired().HasMaxLength(15);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Role).HasConversion<string>();
        }
    }
}
