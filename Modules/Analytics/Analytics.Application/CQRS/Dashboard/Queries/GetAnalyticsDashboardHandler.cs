using Analytics.Application.DTOs;
using Analytics.Application.Interfaces.PriceAnalytics.Service;
using MediatR;

namespace Analytics.Application.CQRS.Dashboard.Queries
{
    public sealed class GetAnalyticsDashboardHandler : IRequestHandler<GetAnalyticsDashboardQuery,AnalyticsDashboardDto>
    {
        private readonly IAnalyticsDashboardService _dashboardService;

        public GetAnalyticsDashboardHandler(
            IAnalyticsDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<AnalyticsDashboardDto> Handle(
            GetAnalyticsDashboardQuery request,
            CancellationToken cancellationToken)
        {
            return await _dashboardService.GetDashboardAsync( request.CategoryCode,request.Source,request.Currency, request.From, request.To,cancellationToken);
        }
    }
}
