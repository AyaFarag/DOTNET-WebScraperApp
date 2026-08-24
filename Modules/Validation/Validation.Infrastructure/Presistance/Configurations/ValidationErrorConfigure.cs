using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validation.Domain.Entities;

namespace Validation.Infrastructure.Presistance.Configurations
{
    public class ValidationErrorConfigure : IEntityTypeConfiguration<ValidationError>
    {
        public void Configure(EntityTypeBuilder<ValidationError> builder)
        {
            builder.ToTable("ValidationErrors", schema: "Validation");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Rule)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Message)
                .HasMaxLength(1000)
                .IsRequired();
           
        }
    }
}
