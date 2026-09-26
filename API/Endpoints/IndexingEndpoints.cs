using Index.Application.DTOs.Search;
using Index.Application.Interfaces.Search;

namespace API.Endpoints
{
    public static class IndexingEndpoints
    {
        public static void MapIndexingEndpoint(this WebApplication app)
        {
            app.MapGet("/api/products/search", async ([AsParameters] ProductSearchRequest request,
                    IProductSearchService searchService,
                    CancellationToken cancellationToken) =>
                {
                    var result = await searchService.SearchAsync(request,cancellationToken);

                    return Results.Ok(result);
            });


            app.MapGet("/api/products/search/ai",async (string query,INaturalLanguageSearchService searchService,
                CancellationToken cancellationToken) =>
            {
                var result =
                    await searchService.SearchAsync(query,cancellationToken);

                return Results.Ok(result);
            });
        } 
    }
}
