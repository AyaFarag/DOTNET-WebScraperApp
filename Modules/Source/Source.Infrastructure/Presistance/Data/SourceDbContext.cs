using Microsoft.EntityFrameworkCore;


namespace Source.Infrastructure.Presistance.Data
{
    public class SourceDbContext : DbContext
    {
        public SourceDbContext(DbContextOptions<SourceDbContext> options) : base(options)
        {
        }

        public DbSet<Source.Domain.Entities.Source> Sources { get; set; } = null!;
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SourceDbContext).Assembly);
        }
    }
}
