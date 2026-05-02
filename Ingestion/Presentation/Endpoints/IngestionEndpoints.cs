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
            app.MapPost("/ingestion/scrape", async (IngestionService service) =>
            {
                //await service.RunScrapingAsync(new ScrapePricesQuery());

                var data  = await service.RunScrapingAsync();
                return data != null ? Results.Ok(data) : Results.Problem("Failed to scrape data");  
            });
        }
    }
}
