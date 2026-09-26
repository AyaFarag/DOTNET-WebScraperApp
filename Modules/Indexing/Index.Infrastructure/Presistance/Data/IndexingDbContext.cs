using Index.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Index.Infrastructure.Presistance.Data
{
    public class IndexingDbContext : DbContext
    {
        public IndexingDbContext(DbContextOptions<IndexingDbContext> options) : base(options)
        {
            
        }

        public DbSet<IndexedProduct> indexedProducts { get; set; }
        public DbSet<IndexingExecution> indexingExecutions { get; set; }
        public DbSet<ProcessedEvent> processedEvents { get; set; }
        public DbSet<OutboxMessage> outboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IndexingDbContext).Assembly);

        }
    }
}
