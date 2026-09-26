using Classification.Domain.Enttities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classification.Infrastructure.Persistence.Configurations
{
    public class ClassificationResultConfiguration : IEntityTypeConfiguration<ClassificationResult>
    {
        public void Configure(EntityTypeBuilder<ClassificationResult> builder)
        {
            builder.ToTable("ClassificationResults", schema: "Classification");
            builder.HasKey(r => r.Id); 
        }
    }
}
