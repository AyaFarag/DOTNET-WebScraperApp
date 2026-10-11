using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class CpiWeightConfiguration
        : IEntityTypeConfiguration<CpiWeight>
    {
        public void Configure(
            EntityTypeBuilder<CpiWeight> builder)
        {
            builder.ToTable("CpiWeights");

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
