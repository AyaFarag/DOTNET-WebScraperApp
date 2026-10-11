using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class PriceWeightConfiguration : IEntityTypeConfiguration<PriceWeight>
    {
        public void Configure(EntityTypeBuilder<PriceWeight> builder)
        {
            builder.ToTable("PriceWeights");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductKey)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .HasMaxLength(500);

            builder.Property(x => x.CategoryCode)
                .HasMaxLength(100);

            builder.Property(x => x.WeightType)
                .IsRequired();

            builder.Property(x => x.Weight)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.EffectiveFrom)
                .IsRequired();

            builder.Property(x => x.EffectiveTo);

            builder.HasIndex(x => new
            {
                x.ProductKey,
                x.EffectiveFrom
            });

            builder.HasIndex(x => new
            {
                x.CategoryCode,
                x.EffectiveFrom
            });
        }
    }
}
