using Analytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Infrastructure.Persistence.Data
{
    public sealed class AnalyticsDbContext : DbContext
    {
        public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : base(options)
        {
        }

        public DbSet<PriceSnapshot> PriceSnapshots => Set<PriceSnapshot>();
        public DbSet<ProcessedEvent> processedEvents { get; set; }
        public DbSet<PriceAnomaly> PriceAnomalies => Set<PriceAnomaly>();
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
        public DbSet<AnalyticsExecution> AnalyticsExecutions => Set<AnalyticsExecution>();
        public DbSet<AnomalyExplanation> AnomalyExplanations => Set<AnomalyExplanation>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnalyticsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
