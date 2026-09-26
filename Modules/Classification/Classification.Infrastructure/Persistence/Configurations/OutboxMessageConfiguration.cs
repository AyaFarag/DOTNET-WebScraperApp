using Classification.Domain.Enttities;
using Microsoft.EntityFrameworkCore;

namespace Classification.Infrastructure.Persistence.Configurations
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("OutboxMessages", schema: "Classification");
            builder.HasKey(m => m.Id);
        }
    }
}
