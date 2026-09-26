using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class PriceAnomalyConfiguration : IEntityTypeConfiguration<PriceAnomaly>
    {
        public void Configure(EntityTypeBuilder<PriceAnomaly> builder)
        {
            builder.ToTable("PriceAnomalies", "Analytics");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductKey)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .HasMaxLength(500);

            builder.Property(x => x.Brand)
                .HasMaxLength(200);

            builder.Property(x => x.CategoryCode)
                .HasMaxLength(100);

            builder.Property(x => x.Source)
                .HasMaxLength(200);

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Severity)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(18, 4);

            builder.Property(x => x.Q1)
                .HasPrecision(18, 4);

            builder.Property(x => x.Median)
                .HasPrecision(18, 4);

            builder.Property(x => x.Q3)
                .HasPrecision(18, 4);

            builder.Property(x => x.Iqr)
                .HasPrecision(18, 4);

            builder.Property(x => x.LowerBound)
                .HasPrecision(18, 4);

            builder.Property(x => x.UpperBound)
                .HasPrecision(18, 4);

            builder.Property(x => x.DeviationPercentage)
                .HasPrecision(18, 4);

            builder.HasIndex(x => x.SnapshotId)
                .IsUnique();

            builder.HasIndex(x => x.ProductKey);

            builder.HasIndex(x => x.CategoryCode);

            builder.HasIndex(x => x.Source);

            builder.HasIndex(x => x.DetectedAt);
            builder.Property(x => x.Direction)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
