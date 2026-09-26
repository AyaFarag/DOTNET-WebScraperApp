using Analytics.Application.DTOs;
using Analytics.Application.Interfaces;
using MediatR;

namespace Analytics.Application.CQRS.PriceStatistics.Queries
{
    public sealed class GetPriceStatisticsHandler : IRequestHandler<GetPriceStatisticsQuery, IReadOnlyCollection<PriceStatisticsDto>>
    {
        private readonly IPriceAnalyticsService _priceAnalyticsService;

        public GetPriceStatisticsHandler(IPriceAnalyticsService priceAnalyticsService)
        {
            _priceAnalyticsService = priceAnalyticsService;
        }

        public async Task<IReadOnlyCollection<PriceStatisticsDto>> Handle(GetPriceStatisticsQuery request,CancellationToken cancellationToken)
        {
            return await _priceAnalyticsService.GetStatisticsAsync(request.CategoryCode, request.Source, request.Currency, cancellationToken);
        }
    }
}
