using API.Scenario.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Scenario.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<PriceRecord> Prices { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
