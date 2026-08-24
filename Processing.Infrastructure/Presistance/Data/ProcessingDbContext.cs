using Microsoft.EntityFrameworkCore;
using Processing.Domain.Entities;

namespace Processing.Infrastructure.Presistance.Data
{
    public sealed class ProcessingDbContext : DbContext
    {
        public ProcessingDbContext(
            DbContextOptions<ProcessingDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProcessedPrice> ProcessedPrices
            => Set<ProcessedPrice>();

        public DbSet<ProcessingError> ProcessingErrors
            => Set<ProcessingError>();

        public DbSet<ProcessedEvent> ProcessedEvents
            => Set<ProcessedEvent>();

        public DbSet<OutboxMessage> OutboxMessages
            => Set<OutboxMessage>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
             typeof(ProcessingDbContext).Assembly);
        }
    }
}