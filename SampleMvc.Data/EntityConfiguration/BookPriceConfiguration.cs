using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMvc.Data.Entity;

namespace SampleMvc.Data.EntityConfiguration
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
