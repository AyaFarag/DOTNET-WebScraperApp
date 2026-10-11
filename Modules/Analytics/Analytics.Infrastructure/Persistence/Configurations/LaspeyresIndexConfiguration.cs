using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class LaspeyresIndexConfiguration
    : IEntityTypeConfiguration<LaspeyresIndex>
    {
        public void Configure(
            EntityTypeBuilder<LaspeyresIndex> builder)
        {
            builder.ToTable("LaspeyresIndexes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.BaseBasketValue)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.CurrentBasketValue)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.IndexValue)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.ChangePercentage)
                .HasPrecision(18, 8);

            builder.HasIndex(x => new
            {
                x.CategoryCode,
                x.Currency,
                x.BaseDate,
                x.CurrentDate
            })
            .IsUnique();
        }
    }
}
