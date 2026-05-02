using API.Scenario.Indexing.Interface;
using API.Scenario.Shared;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace API.Scenario.Endpoint
{
    public static class Endpoints
    {
        public static void MapScenarioEndpoints(this WebApplication app) 
        {
            app.MapGet("/api/cpi", async (IIndexService indexService) =>
            {
                var result = await indexService.Calculate();
                return Results.Ok(result);
            });

            app.MapPost("/api/scrape-and-process", async (IMediator mediator) =>
            {
                // Step 1: Scrape (simulate or call real scraper)
                var scraped = await FakeScraper();

                // Step 2: Publish event
                await mediator.Publish(new PriceScrapedEvent
                {
                    ProductName = scraped.Product,
                    RawPrice = scraped.Price,
                    Source = "DemoSite"
                });

                return Results.Ok("Pipeline triggered");
            });
        }



        public static Task<(string Product, string Price)> FakeScraper()
        {
            // Replace with Playwright in real case
            return Task.FromResult<(string Product, string Price)>(("Rice", "10 AED"));
        }
    }
}
