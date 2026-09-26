using MediatR;
using Microsoft.AspNetCore.Authorization;
using Source.Application.CQRS.Commands;
using Source.Application.CQRS.Queries;
using Source.Application.DTOs;

namespace API.Endpoints
{
    using Asp.Versioning;
    using Asp.Versioning.Builder;

    public static class SourceEndpoints
    {
        /*
         .MapGroup()
        .RequireAuthorization()
        .RequireRateLimiting()
        .WithTags()
        .WithSummary()
        .WithDescription()
        .WithMetadata()
         */
        public static void MapSourceEndpoints(this WebApplication app)
        {
            //var versionSet = app.NewApiVersionSet()
            //    .HasApiVersion(new ApiVersion(1, 0))
            //    .ReportApiVersions()
            //    .Build();
            var sources = app.MapGroup("/api/v1/sources")
              //  .WithApiVersionSet(versionSet)
                .WithTags("Sources")
               // .MapToApiVersion(1, 0)
              //  .RequireAuthorization()
                ;

            sources.MapGet("/", async (IMediator mediator) =>
            {
                var sources = await mediator.Send(new GetAllSourcesQuery());

                return sources != null ? Results.Ok(sources) : Results.Problem("Failed to retrieve sources");
            })
           // .MapToApiVersion(1, 0)
            ;


            sources.MapPost("/", async (IMediator mediator, SourceRequest sourcerequest) =>
            {
                // apply try catch block to handle exceptions
                var source = false;
                try
                {
                   await mediator.Send(new CreateSourceCommand(sourcerequest));
                    source = true;
                }
                catch (Exception ex)
                {
                    source = false;
                }
                return source ? Results.Ok(source) : Results.Problem("Failed to create source");
            })
                //.MapToApiVersion(1, 0)
            ;


            sources.MapPatch("/{id}", async (IMediator mediator, Guid id, UpdateSourceRequest updateSourceRequest) =>
            {
                
                var source = false;
                try
                {
                    await mediator.Send(new UpdateSourceCommand(id, updateSourceRequest));
                    source = true;
                }
                catch (Exception ex)
                {
                    source = false;
                }
                return source ? Results.Ok(source) : Results.Problem("Failed to update source");
            })
               // .MapToApiVersion(1, 0)
                ;

            sources.MapDelete("/{id}", async (IMediator mediator, Guid id) =>
            {
                var result = false;
                 try
                {
                    await mediator.Send(new DeleteSourceCommand(id));
                    result = true;
                }
                catch (Exception ex)
                {
                    
                    result = false;
                }
                return result ? Results.Ok() : Results.Problem("Failed to delete source");
            })
                //.MapToApiVersion(1, 0)
            ;


            sources.MapPost("/{id}/disable", async (IMediator mediator, Guid id) =>
            {
               
                var result =  await mediator.Send(new DisableSourceCommand(id));
                result = true;
              
                return result ? Results.Ok() : Results.Problem("Failed to disable source");
            })
                //.MapToApiVersion(1, 0)
            ;

            sources.MapPost("/{id}/enable", async (IMediator mediator, Guid id) =>
            {
                var result = await mediator.Send(new EnableSourceCommand(id));
                result = true;
                return result ? Results.Ok() : Results.Problem("Failed to enable source");
            })
                //.MapToApiVersion(1, 0)
                ;

        }
    }
}
