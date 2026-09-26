using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class PriceSnapshotConfiguration
    : IEntityTypeConfiguration<PriceSnapshot>
    {
        public void Configure(
            EntityTypeBuilder<PriceSnapshot> builder)
        {
            builder.ToTable("PriceSnapshots");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductKey)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .HasMaxLength(500);

            builder.Property(x => x.Brand)
                .HasMaxLength(200);

            builder.Property(x => x.CategoryCode)
                .HasMaxLength(100);

            builder.Property(x => x.Source)
                .HasMaxLength(200);

            builder.Property(x => x.Price)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasPrecision(18, 4);

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.CollectedAt)
                .IsRequired();

            builder.HasIndex(x => x.ProductKey);

            builder.HasIndex(x => x.CollectedAt);

            builder.HasIndex(x => new
            {
                x.ProductKey,
                x.CollectedAt
            });

            builder.HasIndex(x => x.BatchId);
        }
    }
}
