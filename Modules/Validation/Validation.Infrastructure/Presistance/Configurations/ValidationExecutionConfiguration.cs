using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validation.Domain.Entities;

namespace Validation.Infrastructure.Presistance.Configurations
{
    public class ValidationExecutionConfiguration : IEntityTypeConfiguration<ValidationExecution>
    {
        public void Configure(EntityTypeBuilder<ValidationExecution> builder)
        {
            builder.ToTable("ValidationExecutions", schema: "Validation");
            builder.HasIndex(x => x.BatchId).IsUnique();
        }
    }
}
