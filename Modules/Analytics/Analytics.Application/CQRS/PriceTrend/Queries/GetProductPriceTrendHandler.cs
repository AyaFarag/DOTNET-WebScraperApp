using Analytics.Application.DTOs;
using Analytics.Application.Interfaces;
using MediatR;

namespace Analytics.Application.CQRS.PriceTrend.Queries
{
    public sealed class GetProductPriceTrendHandler : IRequestHandler<GetProductPriceTrendQuery, IReadOnlyCollection<PriceTrendDto>>
    {
        private readonly IPriceTrendService _priceTrendService;

        public GetProductPriceTrendHandler(IPriceTrendService priceTrendService)
        {
            _priceTrendService = priceTrendService;
        }

        public async Task<IReadOnlyCollection<PriceTrendDto>> Handle(GetProductPriceTrendQuery request, CancellationToken cancellationToken)
        {
            return await _priceTrendService.GetProductTrendAsync(
                request.ProductKey,
                request.From,
                request.To,
                request.Source,
                request.Currency,
                request.Granularity,
                cancellationToken);
        }
    }
}
