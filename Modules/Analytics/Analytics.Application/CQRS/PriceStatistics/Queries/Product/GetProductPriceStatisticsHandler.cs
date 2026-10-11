using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using MediatR;

namespace Analytics.Application.CQRS.PriceStatistics.Queries.Product
{
    public sealed class GetProductPriceStatisticsHandler : IRequestHandler<GetProductPriceStatisticsQuery, PriceStatisticsDto?>
    {
        private readonly IPriceAnalyticsService _priceAnalyticsService;

        public GetProductPriceStatisticsHandler(IPriceAnalyticsService priceAnalyticsService)
        {
            _priceAnalyticsService = priceAnalyticsService;
        }

        public async Task<PriceStatisticsDto?> Handle(GetProductPriceStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await _priceAnalyticsService.GetProductStatisticsAsync(request.ProductKey, request.Source, request.Currency, cancellationToken);
        }
    }
}
