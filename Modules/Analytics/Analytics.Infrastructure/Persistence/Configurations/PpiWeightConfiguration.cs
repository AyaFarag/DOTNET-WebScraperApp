using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class PpiWeightConfiguration
        : IEntityTypeConfiguration<PpiWeight>
    {
        public void Configure(
            EntityTypeBuilder<PpiWeight> builder)
        {
            builder.ToTable("PpiWeights");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CategoryName)
                .HasMaxLength(200);

            builder.Property(x => x.Weight)
                .HasPrecision(18, 8);

            builder.HasIndex(x => new
            {
                x.CategoryCode,
                x.EffectiveFrom
            });

            builder.HasIndex(x => x.EffectiveFrom);
        }
    }
}
