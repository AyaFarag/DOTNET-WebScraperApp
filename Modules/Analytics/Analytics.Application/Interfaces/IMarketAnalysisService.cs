using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces
{
    public interface IMarketAnalysisService
    {
        Task<MarketAnalysisDto?> AnalyzeProductAsync(
            string productKey,
            string? currency,
            CancellationToken cancellationToken);
    }
}
