using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceAnalytics.Repository
{
    public interface IAnalyticsDashboardRepository
    {
        Task<AnalyticsDashboardData> GetDashboardDataAsync(
            string? categoryCode,
            string? source,
            string? currency,
            DateTime? from,
            DateTime? to,
            int topCount,
            CancellationToken cancellationToken);
    }
}
