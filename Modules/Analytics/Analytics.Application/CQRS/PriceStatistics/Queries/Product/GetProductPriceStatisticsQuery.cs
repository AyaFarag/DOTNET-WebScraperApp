using Analytics.Application.DTOs;
using MediatR;

namespace Analytics.Application.CQRS.PriceStatistics.Queries.Product
{
    public sealed record GetProductPriceStatisticsQuery(string ProductKey, string? Source,string Currency) : IRequest<PriceStatisticsDto?>;
}
