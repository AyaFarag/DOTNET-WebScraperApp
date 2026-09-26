using Index.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Index.Infrastructure.Presistance.Configurations
{
    public class IndexedProductConfiguration : IEntityTypeConfiguration<IndexedProduct>
    {
        public void Configure(EntityTypeBuilder<IndexedProduct> builder)
        {
            builder.ToTable("indexedProducts", schema: "Index");
        }
    }
}
