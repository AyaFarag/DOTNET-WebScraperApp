using Application.CQRS.Comand;
using Ingestion.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;


namespace Ingestion.Application.Configurations;

    public static class DependencyInjection
    {
        public static IServiceCollection AddIngestionApplication(this IServiceCollection services)
        {
            //services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(typeof(ScrapePricesCommand).Assembly);
            //    cfg.RegisterServicesFromAssembly(typeof(ScrapePricesQuery).Assembly);
            //});

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            
             // Program.cs or your DI extension (csharp)
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssemblyContaining(typeof(ScrapePricesCommandHandler)));
           
           services.AddMediatR(
                cfg =>
                {
                    cfg.RegisterServicesFromAssemblies(
                        typeof(ScrapePricesCommand).Assembly,
                        typeof(ScrapePricesCommandHandler).Assembly);
                });

           // Program.cs or DI extension (csharp)
           services.AddTransient<IRequestHandler<ScrapePricesCommand, string>, ScrapePricesCommandHandler>();
           
         
            services.AddScoped<IIngestionService, IngestionService>();
                return services;
        }
    }
