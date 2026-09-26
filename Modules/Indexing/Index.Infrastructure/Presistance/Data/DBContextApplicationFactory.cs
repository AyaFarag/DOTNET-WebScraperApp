using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Index.Infrastructure.Presistance.Data
{
    public class DBContextApplicationFactory : IDesignTimeDbContextFactory<IndexingDbContext>
    {
        public IndexingDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<IndexingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);


            return new IndexingDbContext(optionsBuilder.Options);
        }
    }
}
