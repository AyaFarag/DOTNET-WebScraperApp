using Analytics.Application.DTOs;
using MediatR;

namespace Analytics.Application.CQRS.Execution
{
    public sealed record GetAnalyticsExecutionsQuery(
    int Page,
    int PageSize,
    string? Status ) : IRequest<PagedResult<AnalyticsExecutionDto>>;
}
