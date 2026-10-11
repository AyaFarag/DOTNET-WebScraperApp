using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class FisherIndexConfiguration
        : IEntityTypeConfiguration<FisherIndex>
    {
        public void Configure(
            EntityTypeBuilder<FisherIndex> builder)
        {
            builder.ToTable("FisherIndexes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CategoryCode)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.LaspeyresValue)
                .HasPrecision(18, 8);

            builder.Property(x => x.PaascheValue)
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
