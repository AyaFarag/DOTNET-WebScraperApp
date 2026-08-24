using Application.CQRS.Comand;
using Ingestion.Application.CQRS.Query;
using Ingestion.Application.Services;
using MediatR;

namespace API.Endpoints
{
    public static class IngestionEndpoints
    {
        public static void MapIngestionEndpoints(this WebApplication app)
        {
            app.MapPost("/ingestion/scrape/v2", async (IMediator mediator) =>
            {
        
                var data  = await mediator.Send(new ScrapePricesCommand());
                //return data != null ? Results.Ok(data) : Results.Problem("Failed to scrape data"); 
                return Results.Ok("Scraping completed");

            });

            app.MapPost("/ingestion/scrape", async (IIngestionService service, CancellationToken cancellationToken) =>
            {
        
                var data  = await service.RunScrapingAsync(cancellationToken);
                return data != null ? Results.Ok(data) : Results.Problem("Failed to scrape data");  
              
            });
            app.MapPost("/ingestion/scrape/repo", async (IMediator mediator, CancellationToken cancellationToken) =>
            {

                var data = await mediator.Send(new ScrapePricesQuery());
                return data != null ? Results.Ok(data) : Results.Problem("Failed to scrape data");

            });
            app.MapPost("/ingestion/scrape/email", async (IngestionService service) =>
            {
        
                var data  = await service.RunEmailScrapingAsync();
                return data != null ? Results.Ok(data) : Results.Problem("Failed to scrape data");  
               
            });
        }
    }
}
