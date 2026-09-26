using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Source.Infrastructure.Presistance.Configurations
{
    public class SourceConfiguration : IEntityTypeConfiguration<Domain.Entities.Source>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Source> builder)
        {
            builder.ToTable("Sources", schema: "Source");
        }
    }
}
