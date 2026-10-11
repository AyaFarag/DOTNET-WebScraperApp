using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class PaascheIndexConfiguration
        : IEntityTypeConfiguration<PaascheIndex>
    {
        public void Configure(
            EntityTypeBuilder<PaascheIndex> builder)
        {
            builder.ToTable("PaascheIndexes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryCode)
                .IsRequired()
                .HasMaxLength(100);

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
                x.CategoryCode,
                x.Currency,
                x.BaseDate,
                x.CurrentDate
            })
            .IsUnique();
        }
    }
}
