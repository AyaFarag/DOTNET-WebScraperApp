using Index.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Index.Infrastructure.Presistance.Configurations
{
    public class IndexingExecutionConfiguration : IEntityTypeConfiguration<IndexingExecution>
    {
        public void Configure(EntityTypeBuilder<IndexingExecution> builder)
        {
            builder.ToTable("indexingExecutions", schema: "Index");
        }
    }
}
