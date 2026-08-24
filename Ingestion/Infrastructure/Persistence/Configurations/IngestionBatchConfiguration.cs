using Ingestion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class IngestionBatchConfiguration : IEntityTypeConfiguration<IngestionBatch>
    {
        public void Configure(EntityTypeBuilder<IngestionBatch> builder)
        {
            builder.ToTable("IngestionBatches", schema: "Ingestion");
        }
    }
}
