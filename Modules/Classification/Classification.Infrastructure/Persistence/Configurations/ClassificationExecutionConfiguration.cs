using Classification.Domain.Enttities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classification.Infrastructure.Persistence.Configurations
{
    public class ClassificationExecutionConfiguration : IEntityTypeConfiguration<ClassificationExecution>
    {
        public void Configure(EntityTypeBuilder<ClassificationExecution> builder)
        {
            builder.ToTable("ClassificationExecutions", schema: "Classification");
            builder.HasKey(e => e.Id);

        }
    }
}
