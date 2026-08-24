using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Validation.Infrastructure.Presistance.Data
{
    public class DBContextApplicationFactory : IDesignTimeDbContextFactory<ValidationDbContext>
    {
        public ValidationDbContext CreateDbContext(string[] args)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");


            var optionsBuilder = new DbContextOptionsBuilder<ValidationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);


            return new ValidationDbContext(optionsBuilder.Options);
        }
    }
}
