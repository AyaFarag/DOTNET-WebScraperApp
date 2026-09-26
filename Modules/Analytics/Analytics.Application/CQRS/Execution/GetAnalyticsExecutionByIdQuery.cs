using Analytics.Application.DTOs;
using MediatR;

namespace Analytics.Application.CQRS.Execution
{
    public sealed record GetAnalyticsExecutionByIdQuery(
    Guid Id ) : IRequest<AnalyticsExecutionDto?>;
}
