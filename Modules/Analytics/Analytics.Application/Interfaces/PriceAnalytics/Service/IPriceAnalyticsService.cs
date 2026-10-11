using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceAnalytics.Service
{
    public interface IPriceAnalyticsService
    {
        Task<PriceStatisticsDto?> GetProductStatisticsAsync(string productKey, string? source, string? currency, CancellationToken cancellationToken);

        Task<IReadOnlyCollection<PriceStatisticsDto>> GetStatisticsAsync(string? categoryCode, string? source, string? currency, CancellationToken cancellationToken);
    }
}
