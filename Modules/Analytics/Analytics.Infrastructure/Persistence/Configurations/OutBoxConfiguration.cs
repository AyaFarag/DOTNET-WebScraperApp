using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public class OutBoxConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", schema: "Analytics");
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
