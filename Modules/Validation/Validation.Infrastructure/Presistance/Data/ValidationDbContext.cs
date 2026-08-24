using Microsoft.EntityFrameworkCore;
using Validation.Domain.Entities;

namespace Validation.Infrastructure.Presistance.Data
{
    public class ValidationDbContext : DbContext
    {
        public ValidationDbContext(DbContextOptions<ValidationDbContext> options) : base(options)
        {

        }

        public DbSet<ProcessedEvent> ProcessedEvents => Set<ProcessedEvent>();
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
        public DbSet<ValidationResult> ValidationResults => Set<ValidationResult>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ValidationDbContext).Assembly);
        }
    }
}
