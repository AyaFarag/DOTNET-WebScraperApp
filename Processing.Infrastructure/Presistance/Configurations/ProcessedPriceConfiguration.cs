using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Domain.Entities;

namespace Processing.Infrastructure.Presistance.Configurations
{
    public class ProcessedPriceConfiguration : IEntityTypeConfiguration<ProcessedPrice>
    {
        public void Configure(EntityTypeBuilder<ProcessedPrice> builder)
        {
            builder.ToTable("ProcessedPrices", schema: "processing");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Brand)
                .HasMaxLength(200);

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Source)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasPrecision(18, 4);

            builder.Property(x => x.UnitPrice)
                .HasPrecision(18, 6);

            builder.Property(x => x.Quantity)
                .HasPrecision(18, 6);

            builder.Property(x => x.NormalizedQuantity)
                .HasPrecision(18, 6);

            builder.HasIndex(x => x.BatchId);

            builder.HasIndex(x => x.RawPriceId)
                .IsUnique();

            builder.HasMany<ProcessingError>()
                .WithOne(x => x.ProcessedPrice)
                .HasForeignKey(x => x.ProcessedPriceId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
