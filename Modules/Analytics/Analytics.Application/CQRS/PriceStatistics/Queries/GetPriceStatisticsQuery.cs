using Analytics.Application.DTOs;
using MediatR;

namespace Analytics.Application.CQRS.PriceStatistics.Queries
{
    public sealed record GetPriceStatisticsQuery(
    string? CategoryCode,
    string? Source,
    string? Currency
    ) : IRequest<IReadOnlyCollection<PriceStatisticsDto>>;
}
