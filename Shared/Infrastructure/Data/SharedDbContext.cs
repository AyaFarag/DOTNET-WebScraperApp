using Microsoft.EntityFrameworkCore;
using Shared.Domain;

namespace Shared.Infrastructure.Data
{
    public class SharedDbContext : DbContext
    {
        public SharedDbContext(DbContextOptions<SharedDbContext> options) : base(options)
        {
            
        }

        public DbSet<RawPrice> RawPrices { get; private set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SharedDbContext).Assembly);
        }
    }
}
