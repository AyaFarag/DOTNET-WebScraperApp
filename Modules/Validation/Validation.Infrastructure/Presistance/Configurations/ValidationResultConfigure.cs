using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validation.Domain.Entities;

namespace Validation.Infrastructure.Presistance.Configurations
{
    public class ValidationResultConfigure : IEntityTypeConfiguration<ValidationResult>
    {
        public void Configure(EntityTypeBuilder<ValidationResult> builder)
        {
            builder.ToTable("ValidationResults", schema: "Validation");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BatchId)
                .IsRequired();

            builder.Property(x => x.RawPriceId)
                .IsRequired();

            builder.Property(x => x.IsValid)
                .IsRequired();

            builder.HasMany(x => x.Errors)
                .WithOne(x => x.ValidationResult)
                .HasForeignKey(x => x.ValidationResultId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new
            {
                x.BatchId,
                x.RawPriceId
            });
        }
    }
}
