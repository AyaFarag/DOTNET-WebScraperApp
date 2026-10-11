using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{

    public sealed class PriceRelativeConfiguration
        : IEntityTypeConfiguration<PriceRelative>
    {
        public void Configure(EntityTypeBuilder<PriceRelative> builder)
        {
            builder.ToTable("PriceRelatives");

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

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.BasePrice)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(x => x.CurrentPrice)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(x => x.RelativeValue)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(x => x.BaseDate)
                .IsRequired();

            builder.Property(x => x.CurrentDate)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.ProductKey,
                x.Source,
                x.Currency,
                x.BaseDate,
                x.CurrentDate
            });
        }
    }
}
