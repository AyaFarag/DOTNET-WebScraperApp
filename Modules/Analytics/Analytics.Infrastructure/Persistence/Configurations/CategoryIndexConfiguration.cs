using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class CategoryIndexConfiguration
    : IEntityTypeConfiguration<CategoryIndex>
    {
        public void Configure(
            EntityTypeBuilder<CategoryIndex> builder)
        {
            builder.ToTable("CategoryIndexes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.BaseWeightedPrice)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.CurrentWeightedPrice)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.IndexValue)
                .HasPrecision(18, 8)
                .IsRequired();

            builder.Property(x => x.ChangePercentage)
                .HasPrecision(18, 8);

            builder.Property(x => x.BaseDate)
                .IsRequired();

            builder.Property(x => x.CurrentDate)
                .IsRequired();

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
