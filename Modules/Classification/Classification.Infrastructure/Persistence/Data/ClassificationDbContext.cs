using Classification.Domain.Enttities;
using Classification.Infrastructure.Persistence.Data.Seeding;
using Microsoft.EntityFrameworkCore;

namespace Classification.Infrastructure.Persistence.Data
{
    public class ClassificationDbContext : DbContext
    {
        public ClassificationDbContext(DbContextOptions<ClassificationDbContext> options) : base(options)
        {
        }

        public DbSet<ClassificationCategory> ClassificationCategories { get; set; }
        public DbSet<ProcessedEvent> ProcessedEvents { get; set; }
        public DbSet<ClassificationExecution> ClassificationExecutions { get; set; }
        public DbSet<ClassificationResult> ClassificationResults { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<AgentExecutionLog> AgentExecutionLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClassificationDbContext).Assembly);
            ClassificationCategoryConfiguration.Seed(modelBuilder);
        }
    }
}
