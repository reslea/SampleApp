using System;
using System.Collections.Generic;
using System.Text;
using Library.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.EntityConfiguration
{
    public class BookPriceConfiguration : IEntityTypeConfiguration<BookPrice>
    {
        public void Configure(EntityTypeBuilder<BookPrice> builder)
        {
            builder.HasKey(_ => _.Id);

            builder
                .HasOne(_ => _.Book)
                .WithOne(_ => _.BookPrice)
                .HasForeignKey<BookPrice>(_ => _.BookId);

            builder
                .HasIndex(_ => _.BookId)
                .IsUnique();
        }
    }
}
