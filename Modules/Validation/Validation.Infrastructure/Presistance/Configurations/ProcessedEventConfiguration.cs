using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validation.Domain.Entities;

namespace Validation.Infrastructure.Presistance.Configurations
{
    public class ProcessedEventConfiguration : IEntityTypeConfiguration<ProcessedEvent>
    {
        public void Configure(EntityTypeBuilder<ProcessedEvent> builder)
        {
            builder.ToTable("ProcessedEvents", schema: "Validation");
            builder.HasKey(x => x.EventId);

            builder.Property(x => x.EventType)
                .IsRequired();

            builder.HasIndex(x => x.EventId)
                .IsUnique();

        }
    }
}
