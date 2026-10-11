using Analytics.Application.DTOs;
using Analytics.Domain.Enums;

namespace Analytics.Application.Interfaces.PriceAnalytics.Service
{
    public interface IPriceTrendService
    {
        Task<IReadOnlyCollection<PriceTrendDto>>
            GetProductTrendAsync(
                string productKey,
                DateTime from,
                DateTime to,
                string? source,
                string? currency,
                TrendGranularity granularity,
                CancellationToken cancellationToken);
    }
}
