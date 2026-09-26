using Analytics.Application.DTOs;
using MediatR;

namespace Analytics.Application.CQRS.MarketAnalysis.Queries
{
    public sealed record GetMarketAnalysisQuery(
    string ProductKey,
    string? Currency
    ) : IRequest<MarketAnalysisDto?>;
}
