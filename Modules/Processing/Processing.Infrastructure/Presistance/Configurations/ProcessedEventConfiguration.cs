using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Domain.Entities;

namespace Processing.Infrastructure.Presistance.Configurations
{
    public class ProcessedEventConfiguration : IEntityTypeConfiguration<ProcessedEvent>
    {
        public void Configure(EntityTypeBuilder<ProcessedEvent> builder)
        {
            builder.ToTable("ProcessedEvents", schema: "processing");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.EventType)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasIndex(x => x.EventId)
                .IsUnique();
        }
    }
}
