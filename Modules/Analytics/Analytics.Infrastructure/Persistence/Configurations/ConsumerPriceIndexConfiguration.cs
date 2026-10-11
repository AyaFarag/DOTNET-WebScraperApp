using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class ConsumerPriceIndexConfiguration
        : IEntityTypeConfiguration<ConsumerPriceIndex>
    {
        public void Configure(
            EntityTypeBuilder<ConsumerPriceIndex> builder)
        {
            builder.ToTable("ConsumerPriceIndexes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.BaseBasketValue)
                .HasPrecision(18, 8);

            builder.Property(x => x.CurrentBasketValue)
                .HasPrecision(18, 8);

            builder.Property(x => x.IndexValue)
                .HasPrecision(18, 8);

            builder.Property(x => x.ChangePercentage)
                .HasPrecision(18, 8);

            builder.HasIndex(x => new
            {
                x.Currency,
                x.BaseDate,
                x.CurrentDate
            })
            .IsUnique();
        }
    }
}
