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
        public DbSet<PriceRelative> PriceRelatives => Set<PriceRelative>();
        public DbSet<PriceWeight> PriceWeights => Set<PriceWeight>();
        public DbSet<WeightedPrice> WeightedPrices => Set<WeightedPrice>();
        public DbSet<CategoryIndex> CategoryIndexes => Set<CategoryIndex>();
        public DbSet<LaspeyresIndex> LaspeyresIndexes => Set<LaspeyresIndex>();
        public DbSet<PaascheIndex> PaascheIndexes => Set<PaascheIndex>();
        public DbSet<FisherIndex> FisherIndexes => Set<FisherIndex>();
        public DbSet<CpiWeight> CpiWeights => Set<CpiWeight>();
        public DbSet<PpiWeight> PpiWeights => Set<PpiWeight>();
        public DbSet<ConsumerPriceIndex> ConsumerPriceIndexes => Set<ConsumerPriceIndex>();
        public DbSet<ProducerPriceIndex> ProducerPriceIndexes => Set<ProducerPriceIndex>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnalyticsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
