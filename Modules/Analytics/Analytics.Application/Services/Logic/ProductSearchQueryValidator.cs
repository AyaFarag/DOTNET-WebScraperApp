using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.AI.Validator;

namespace Analytics.Application.Services.Logic
{
    public sealed class ProductSearchQueryValidator : IProductSearchQueryValidator
    {
        public void Validate(ProductSearchQueryPlan plan)
        {
            if (plan.MinimumPrice.HasValue &&
                plan.MaximumPrice.HasValue &&
                plan.MinimumPrice > plan.MaximumPrice)
            {
                throw new InvalidOperationException(
                    "Minimum price cannot be greater than maximum price.");
            }

            if (plan.Page < 1)
            {
                throw new InvalidOperationException(
                    "Page must be greater than zero.");
            }

            if (plan.PageSize is < 1 or > 100)
            {
                throw new InvalidOperationException(
                    "Page size must be between 1 and 100.");
            }
        }
    }
}
