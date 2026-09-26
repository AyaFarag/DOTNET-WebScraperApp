using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Domain.Entities;

namespace Processing.Infrastructure.Presistance.Configurations
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", schema: "processing");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.EventType)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Payload)
                .IsRequired();

            builder.Property(x => x.Error)
                .HasMaxLength(2000);

            builder.HasIndex(x => x.EventId)
                .IsUnique();

            builder.HasIndex(x => new
            {
                x.ProcessedOnUtc,
                x.OccurredOnUtc
            });
        }
    }
}
