using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using MediatR;

namespace Analytics.Application.CQRS.MarketAnalysis.Queries
{
    public sealed class GetMarketAnalysisHandler  : IRequestHandler<GetMarketAnalysisQuery, MarketAnalysisDto?>
    {
        private readonly IMarketAnalysisService _marketAnalysisService;

        public GetMarketAnalysisHandler(IMarketAnalysisService marketAnalysisService)
        {
            _marketAnalysisService = marketAnalysisService;
        }

        public async Task<MarketAnalysisDto?> Handle(GetMarketAnalysisQuery request, CancellationToken cancellationToken)
        {
            return await _marketAnalysisService.AnalyzeProductAsync(request.ProductKey, request.Currency, cancellationToken);
        }
    }
}
