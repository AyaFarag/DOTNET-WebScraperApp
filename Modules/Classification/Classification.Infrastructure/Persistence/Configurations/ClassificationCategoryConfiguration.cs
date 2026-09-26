using Classification.Domain.Enttities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Classification.Infrastructure.Persistence.Configurations
{
    public class ClassificationCategoryConfiguration : IEntityTypeConfiguration<ClassificationCategory>
    {
        public void Configure(EntityTypeBuilder<ClassificationCategory> builder)
        {
            builder.ToTable("ClassificationCategories", schema: "Classification");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
