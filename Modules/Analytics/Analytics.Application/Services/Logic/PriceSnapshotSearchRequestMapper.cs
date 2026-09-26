using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.Logic;

namespace Analytics.Application.Services.Logic
{
    public sealed class PriceSnapshotSearchRequestMapper
    : IPriceSnapshotSearchRequestMapper
    {
        private readonly IDateRangeResolver _dateRangeResolver;

        public PriceSnapshotSearchRequestMapper(
            IDateRangeResolver dateRangeResolver)
        {
            _dateRangeResolver = dateRangeResolver;
        }

        public PriceSnapshotSearchRequest Map(
            AnalyticsSearchQueryPlan plan,
            DateTime now)
        {
            var (from, to) =
                _dateRangeResolver.Resolve(
                    plan.DateRangeType,
                    plan.From,
                    plan.To,
                    now);

            return new PriceSnapshotSearchRequest
            {
                ProductKey = plan.ProductKey,
                ProductName = plan.ProductName,
                Brand = plan.Brand,
                CategoryCode = plan.CategoryCode,
                Source = plan.Source,
                Currency = plan.Currency,
                MinimumPrice = plan.MinimumPrice,
                MaximumPrice = plan.MaximumPrice,
                From = from,
                To = to,
                Page = plan.Page,
                PageSize = plan.PageSize
            };
        }
    }
}
