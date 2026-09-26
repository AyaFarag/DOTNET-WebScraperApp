using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts.Queries.Source;
using Source.Application.Interfaces.Repository;
using Source.Infrastructure.Presistance.Data;
using Source.Infrastructure.Presistance.Repository;

namespace Source.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSourceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<SourceDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ISourceRepository, SourceRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ISourceQueryReader<>), typeof(SourceQueryReader<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
