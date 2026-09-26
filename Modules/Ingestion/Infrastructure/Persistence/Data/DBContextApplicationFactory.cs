using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Persistence.Data
{
    public class DBContextApplicationFactory : IDesignTimeDbContextFactory<IngestionDbContext>
    {
        public IngestionDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<IngestionDbContext>();
            optionsBuilder.UseSqlServer(connectionString);


            return new IngestionDbContext(optionsBuilder.Options);
        }
    }
}
