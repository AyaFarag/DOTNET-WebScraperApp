using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class WeightedPriceConfiguration
    : IEntityTypeConfiguration<WeightedPrice>
    {
        public void Configure(
            EntityTypeBuilder<WeightedPrice> builder)
        {
            builder.ToTable("WeightedPrices");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductKey)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .HasMaxLength(500);

            builder.Property(x => x.CategoryCode)
                .HasMaxLength(100);

            builder.Property(x => x.Source)
                .HasMaxLength(200);

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(x => x.Weight)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.WeightedValue)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.PriceDate)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.ProductKey,
                x.Source,
                x.Currency,
                x.PriceDate
            });

            builder.HasIndex(x => new
            {
                x.CategoryCode,
                x.PriceDate
            });
        }
    }
}
