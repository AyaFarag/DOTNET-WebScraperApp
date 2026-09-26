using Ingestion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class OutBoxConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
           builder.ToTable("OutboxMessages", schema: "Ingestion");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                    .IsRequired();

            builder.Property(x => x.Payload)
                    .IsRequired();
            builder.Property(x => x.Error)
                .HasMaxLength(4000);

            builder.HasIndex(x => x.ProcessedOnUtc);
           
        }
    }
}
