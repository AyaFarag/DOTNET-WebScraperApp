using Classification.Domain.Enttities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classification.Infrastructure.Persistence.Configurations
{
    public class AgentExecutionLogConfiguration : IEntityTypeConfiguration<AgentExecutionLog>
    {
        public void Configure(EntityTypeBuilder<AgentExecutionLog> builder)
        {
            builder.ToTable("AgentExecutionLogs", schema: "Classification");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AgentName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Model)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PromptVersion)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Error)
                .HasMaxLength(2000);

            builder.HasIndex(x => x.BatchId);

            builder.HasIndex(x => x.EntityId);

            builder.HasIndex(x => x.StartedAt);
        }

    }
}
