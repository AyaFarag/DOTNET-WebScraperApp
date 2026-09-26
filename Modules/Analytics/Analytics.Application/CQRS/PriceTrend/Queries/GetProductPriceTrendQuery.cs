using Analytics.Application.DTOs;
using Analytics.Domain.Enums;
using MediatR;

namespace Analytics.Application.CQRS.PriceTrend.Queries
{
    public sealed record GetProductPriceTrendQuery(
    string ProductKey,
    DateTime From,
    DateTime To,
    string? Source,
    string? Currency,
    TrendGranularity Granularity
) : IRequest<IReadOnlyCollection<PriceTrendDto>>;
}
