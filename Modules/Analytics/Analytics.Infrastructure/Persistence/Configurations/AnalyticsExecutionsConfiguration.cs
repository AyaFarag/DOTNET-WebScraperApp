using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Analytics.Infrastructure.Persistence.Configurations
{
    public class AnalyticsExecutionsConfiguration : IEntityTypeConfiguration<AnalyticsExecution>
    {
        public void Configure(EntityTypeBuilder<AnalyticsExecution> builder)
        {
            builder.ToTable("AnalyticsExecutions", schema: "Analytics");
        }
    }
}
