using Microsoft.Extensions.DependencyInjection;
using Source.Application.Common;
using Source.Application.CQRS.Commands;
using Source.Application.CQRS.Commands.Handler;
using Source.Application.CQRS.Queries;
using Source.Application.CQRS.Queries.Handler;
using Source.Application.Interfaces.Service;
using Source.Application.Service;
using System.Reflection.Metadata;

namespace Source.Application.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSourceApplication(this IServiceCollection services)
        {
            // Register application services here

            services.AddAutoMapper(cfg => { }, typeof(SourceProfile).Assembly);

            services.AddScoped<ISourceService , SourceService>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    typeof(AssemblyReference).Assembly);
            });

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateSourceCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(CreateSourceCommandHandler).Assembly);

                cfg.RegisterServicesFromAssemblies(typeof(UpdateSourceCommand).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(UpdateSourceCommandHandler).Assembly);

                cfg.RegisterServicesFromAssemblies(typeof(DeleteSourceCommand).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(DeleteSourceCommandHandler).Assembly);

                cfg.RegisterServicesFromAssemblies(typeof(EnableSourceCommand).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(EnableSourceCommandHandler).Assembly);

                cfg.RegisterServicesFromAssemblies(typeof(DisableSourceCommand).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(DisableSourceCommandHandler).Assembly);

                cfg.RegisterServicesFromAssemblies(typeof(GetAllSourcesQuery).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(GetAllSourcesQueryHandler).Assembly);

                cfg.RegisterServicesFromAssemblies(typeof(GetSourceByIdQuery).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(GetSourceByIdQueryHandler).Assembly);


            });

            return services;
        }
    }
}
