using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleMvc.Data.Entity;

namespace SampleMvc.Data.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.HasIndex(_ => _.Title);

            builder.HasIndex(_ => new { _.Title, _.Author }).IsUnique();

            builder.Property(_ => _.Author).IsRequired().HasMaxLength(255);

            //builder.Property(_ => _.Genre).IsRequired().HasMaxLength(255);

            builder.Property(_ => _.Title).IsRequired().HasMaxLength(255);
        }
    }
}
