using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public sealed class AnomalyExplanationConfiguration
    : IEntityTypeConfiguration<AnomalyExplanation>
    {
        public void Configure(
            EntityTypeBuilder<AnomalyExplanation> builder)
        {
            builder.ToTable("AnomalyExplanations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Summary)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Explanation)
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(x => x.PossibleInterpretation)
                .HasMaxLength(2000);

            builder.Property(x => x.Confidence)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Model)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PromptVersion)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.AnomalyId)
                .IsUnique();

            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
