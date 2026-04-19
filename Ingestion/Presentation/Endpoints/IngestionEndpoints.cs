using Ingestion.Application.CQRS.Query;
using Ingestion.Application.Services;

namespace Ingestion.Presentation.Endpoints
{
    public static class IngestionEndpoints
    {
        public static void MapIngestionEndpoints(this WebApplication app)
        {
            app.MapPost("/ingestion/scrape", async (IngestionService service) =>
            {
                await service.RunScrapingAsync(new ScrapePricesQuery());
                return Results.Ok("Scraping completed");
            });
        }
    }
}
