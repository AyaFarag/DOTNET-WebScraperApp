using Ingestion.Application.DTOs;


namespace Ingestion.Application.Services
{
    public interface IIngestionService
    {
        Task<List<RawPriceDto>> ScrapeAsync(string source, CancellationToken cancellationToken);
        Task<List<RawPriceDto>> RunScrapingAsync(CancellationToken cancellationToken);

        Task<HashSet<string>> RunEmailScrapingAsync();
    }
}