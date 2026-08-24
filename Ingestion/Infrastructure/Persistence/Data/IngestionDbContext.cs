using Ingestion.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Data;


public class IngestionDbContext : DbContext
{
    public IngestionDbContext(
        DbContextOptions<IngestionDbContext> options)
        : base(options)
    {
    }

    public DbSet<IngestionBatch> IngestionBatches => Set<IngestionBatch>();
    public DbSet<IngestionExecution> IngestionExecutions => Set<IngestionExecution>();
    public DbSet<RawPrice> RawPrices => Set<RawPrice>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();



    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IngestionDbContext).Assembly);
    }
}
