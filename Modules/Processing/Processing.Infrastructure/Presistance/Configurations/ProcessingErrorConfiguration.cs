using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Processing.Domain.Entities;

namespace Processing.Infrastructure.Presistance.Configurations
{
    public class ProcessingErrorConfiguration : IEntityTypeConfiguration<ProcessingError>
    {
        public void Configure(EntityTypeBuilder<ProcessingError> builder)
        {
            builder.ToTable("ProcessingErrors", schema: "processing");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Step)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Message)
                .HasMaxLength(2000)
                .IsRequired();
        }
    }
}
