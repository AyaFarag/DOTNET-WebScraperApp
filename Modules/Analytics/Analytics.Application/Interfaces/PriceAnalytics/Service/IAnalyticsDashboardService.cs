using Analytics.Application.DTOs;

namespace Analytics.Application.Interfaces.PriceAnalytics.Service
{
    public interface IAnalyticsDashboardService
    {
        Task<AnalyticsDashboardDto> GetDashboardAsync(
            string? categoryCode,
            string? source,
            string? currency,
            DateTime? from,
            DateTime? to,
            CancellationToken cancellationToken);
    }
}
