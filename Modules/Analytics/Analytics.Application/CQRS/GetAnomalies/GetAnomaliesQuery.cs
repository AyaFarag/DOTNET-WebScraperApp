using Analytics.Application.DTOs;
using MediatR;

namespace Analytics.Application.CQRS.GetAnomalies
{
    public sealed record GetAnomaliesQuery(
    string? ProductKey,
    string? CategoryCode,
    string? Source,
    string? Severity,
    string? Direction,
    DateTime? From,
    DateTime? To,
    int Page = 1,
    int PageSize = 20
    ) : IRequest<PagedResult<PriceAnomalyDto>>;
}
