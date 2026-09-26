using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Source.Infrastructure.Presistance.Data
{
    public class DBContextApplicationFactory : IDesignTimeDbContextFactory<SourceDbContext>
    {
        public SourceDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<SourceDbContext>();
            optionsBuilder.UseSqlServer(connectionString);


            return new SourceDbContext(optionsBuilder.Options);
        }
    }
}
