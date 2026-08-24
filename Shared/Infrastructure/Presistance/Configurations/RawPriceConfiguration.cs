using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain;

namespace Shared.Infrastructure.Presistance.Configurations;

public class RawPriceConfiguration
    : IEntityTypeConfiguration<RawPrice>
{
    public void Configure(
        EntityTypeBuilder<RawPrice> builder)
    {
        builder.ToTable("RawPrices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Source)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.SourceUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.RawPriceValue)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Currency)
            .HasMaxLength(10);

        builder.Property(x => x.RawData)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.CollectedAt)
            .IsRequired();

        builder.HasIndex(x => x.BatchId);

        builder.HasIndex(x => x.Source);
    }
}
