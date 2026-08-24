using Ingestion.Application.CQRS.Query;
using Ingestion.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ingestion.Presentation.Endpoints
{
    public static class IngestionEndpoints
    {
        public static void MapIngestionEndpoints(this WebApplication app)
        {
            app.MapPost("/ingestion/scrape", async (IIngestionService service) =>
            {
        
                var data  = await service.RunScrapingAsync();
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
