using Ingestion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class IngestionExecutionConfiguration
    : IEntityTypeConfiguration<IngestionExecution>
{
    public void Configure(
        EntityTypeBuilder<IngestionExecution> builder)
    {
        builder.ToTable("IngestionExecutions", schema: "Ingestion");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Source)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(2000);

        builder.HasIndex(x => x.BatchId);
    }
}
