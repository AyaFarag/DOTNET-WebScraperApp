using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Classification.Infrastructure.Persistence.Data
{
    public class DBContextApplicationFactory : IDesignTimeDbContextFactory<ClassificationDbContext>
    {
        public ClassificationDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<ClassificationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);


            return new ClassificationDbContext(optionsBuilder.Options);
        }
    }
}
