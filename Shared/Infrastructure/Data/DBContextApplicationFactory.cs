using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Shared.Infrastructure.Data
{
    public class DBContextApplicationFactory
    {
        public SharedDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<SharedDbContext>();
            optionsBuilder.UseSqlServer(connectionString);


            return new SharedDbContext(optionsBuilder.Options);
        }
    }
}
