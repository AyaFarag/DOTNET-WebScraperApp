using Hangfire;
using Ingestion.Application.DTOs;
using Ingestion.Application.Services;

namespace Infrastructure.Services.BackgroundJobs
{
    public sealed class ScrapingJob
    {
        private readonly IIngestionService _ingestionService;

        public ScrapingJob(IIngestionService ingestionService)
        {
            _ingestionService = ingestionService;
        }

        [AutomaticRetry(Attempts = 3, DelaysInSeconds = new[] { 60, 300, 900 } ) ]
        public async Task<List<RawPriceDto>> ExecuteAsync(CancellationToken cancellationToken)
        {
            return await _ingestionService.RunScrapingAsync(cancellationToken);
        }
    }

}
