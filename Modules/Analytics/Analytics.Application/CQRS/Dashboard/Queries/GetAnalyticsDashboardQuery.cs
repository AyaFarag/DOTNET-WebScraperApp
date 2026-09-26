using Analytics.Application.DTOs;
using MediatR;

namespace Analytics.Application.CQRS.Dashboard.Queries
{
    public sealed record GetAnalyticsDashboardQuery(
    string? CategoryCode,
    string? Source,
    string? Currency,
    DateTime? From,
    DateTime? To) : IRequest<AnalyticsDashboardDto>;
}
