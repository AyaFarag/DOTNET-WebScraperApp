using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.Logic;
using Analytics.Application.Interfaces.AI.PriceSnapshotSearch;

namespace Analytics.Application.Services.AI.Logic
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
                    plan.rangeType,
                    plan.From,
                    plan.To,
                    now);

            return new PriceSnapshotSearchRequest
            {
                ProductKey = plan.ProductKey,
                ProductName = plan.ProductName,
                CategoryCode = plan.CategoryCode,
                Source = plan.Source,
                Currency = plan.Currency,
                From = from,
                To = to
            };
        }
    }
}
