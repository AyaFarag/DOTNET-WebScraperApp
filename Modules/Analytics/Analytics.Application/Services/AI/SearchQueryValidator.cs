using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.SearchQuery;

namespace Analytics.Application.Services.AI
{
    public sealed class SearchQueryValidator : ISearchQueryValidator
    {
        public ProductSearchRequest Validate(
            SearchQueryPlan plan)
        {
            if (plan.Page < 1)
                throw new ArgumentException(
                    "Page must be greater than zero.");

            if (plan.PageSize < 1 ||
                plan.PageSize > 100)
                throw new ArgumentException(
                    "Page size must be between 1 and 100.");

            if (plan.MinimumPrice.HasValue &&
                plan.MaximumPrice.HasValue &&
                plan.MinimumPrice >
                plan.MaximumPrice)
            {
                throw new ArgumentException(
                    "Minimum price cannot exceed maximum price.");
            }

            return new ProductSearchRequest
            {
                SearchText = plan.SearchText,

                ProductKey = plan.ProductKey,

                Brand = plan.Brand,

                CategoryCode = plan.CategoryCode,

                Source = plan.Source,

                Currency = plan.Currency,

                MinimumPrice = plan.MinimumPrice,

                MaximumPrice = plan.MaximumPrice,

                Page = plan.Page,

                PageSize = plan.PageSize
            };
        }
    }
}
