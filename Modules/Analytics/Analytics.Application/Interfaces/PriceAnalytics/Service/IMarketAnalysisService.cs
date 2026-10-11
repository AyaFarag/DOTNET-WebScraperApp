using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceAnalytics.Service
{
    public interface IMarketAnalysisService
    {
        Task<MarketAnalysisDto?> AnalyzeProductAsync(
            string productKey,
            string? currency,
            CancellationToken cancellationToken);
    }
}
