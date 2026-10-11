using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Repository;
using Analytics.Application.Interfaces.PriceAnalytics.Service;

namespace Analytics.Application.Services.PriceAnalytics
{
    public sealed class AnalyticsDashboardService : IAnalyticsDashboardService
    {
        private readonly IAnalyticsDashboardRepository _repository;

        public AnalyticsDashboardService(
            IAnalyticsDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<AnalyticsDashboardDto> GetDashboardAsync(
            string? categoryCode,
            string? source,
            string? currency,
            DateTime? from,
            DateTime? to,
            CancellationToken cancellationToken)
        {
            var data = await _repository.GetDashboardDataAsync(
                categoryCode,
                source,
                currency,
                from,
                to,
                topCount: 5,
                cancellationToken);

            return new AnalyticsDashboardDto
            {
                TotalProducts = data.TotalProducts,
                TotalSources = data.TotalSources,
                AverageMarketPrice = data.AverageMarketPrice,
                TotalAnomalies = data.TotalAnomalies,
                CriticalAnomalies = data.CriticalAnomalies,
                HighPriceAnomalies = data.HighPriceAnomalies,
                LowPriceAnomalies = data.LowPriceAnomalies,
                TopPriceIncreases = data.TopPriceIncreases,
                TopPriceDecreases = data.TopPriceDecreases
            };
        }
    }
}
