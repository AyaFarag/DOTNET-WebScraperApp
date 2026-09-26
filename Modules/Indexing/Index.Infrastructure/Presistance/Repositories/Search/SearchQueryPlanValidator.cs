using Index.Application.DTOs.Search;
using Index.Application.Interfaces.Search;
using Shared.Contracts.Queries.Indexing;

namespace Index.Infrastructure.Presistance.Repositories.Search
{
    public sealed class SearchQueryPlanValidator : ISearchQueryPlanValidator
    {
        private readonly ISearchTaxonomyReader _taxonomyReader;
        public SearchQueryPlanValidator(ISearchTaxonomyReader taxonomyReader)
        {
            _taxonomyReader = taxonomyReader;
        }
        private const int MaxPageSize = 100;

        private static readonly HashSet<string> AllowedSortFields =
            new(StringComparer.OrdinalIgnoreCase)
            {
            "price",
            "unitPrice",
            "collectedAt",
            "productName"
            };

        private static readonly HashSet<string> AllowedCurrencies =
            new(StringComparer.OrdinalIgnoreCase)
            {
            "AED",
            "EGP",
            "SAR",
            "USD"
            };

        public async Task<SearchQueryPlan> ValidateAsync(SearchQueryPlan plan, CancellationToken cancellationToken)
        {
            if (plan is null)
            {
                throw new ArgumentNullException(nameof(plan));
            }

            // -----------------------------
            // 1. Pagination
            // -----------------------------

            var page = plan.Page < 1 ? 1 : plan.Page;

            var pageSize = plan.PageSize switch
            {
                < 1 => 20,
                > MaxPageSize => MaxPageSize,
                _ => plan.PageSize
            };


            // -----------------------------
            // 2. Price validation
            // -----------------------------

            if (plan.MinPrice.HasValue && plan.MinPrice.Value < 0)
            {
                throw new InvalidOperationException(
                    "Minimum price cannot be negative.");
            }

            if (plan.MaxPrice.HasValue &&plan.MaxPrice.Value < 0)
            {
                throw new InvalidOperationException(
                    "Maximum price cannot be negative.");
            }

            if (plan.MinPrice.HasValue &&
                plan.MaxPrice.HasValue &&
                plan.MinPrice.Value > plan.MaxPrice.Value)
            {
                throw new InvalidOperationException(
                    "Minimum price cannot be greater than maximum price.");
            }


            // -----------------------------
            // 3. Currency validation
            // -----------------------------

            string? currency = null;

            if (!string.IsNullOrWhiteSpace(plan.Currency))
            {
                currency = plan.Currency.Trim().ToUpperInvariant();

                if (!AllowedCurrencies.Contains(currency))
                {
                    throw new InvalidOperationException(
                        $"Unsupported currency: {currency}");
                }
            }


            // -----------------------------
            // 4. Sort validation
            // -----------------------------

            string? sortBy = null;

            if (!string.IsNullOrWhiteSpace(plan.SortBy))
            {
                sortBy = plan.SortBy.Trim();

                if (!AllowedSortFields.Contains(sortBy))
                {
                    throw new InvalidOperationException(
                        $"Sorting by '{sortBy}' is not allowed.");
                }

                sortBy = sortBy.ToLowerInvariant();
            }


            // -----------------------------
            // 5. Text normalization
            // -----------------------------

            var searchText = string.IsNullOrWhiteSpace(plan.SearchText) ? null : plan.SearchText.Trim();

            var categoryCode = Normalize(plan.CategoryCode);
            if (!string.IsNullOrWhiteSpace(plan.CategoryCode))
            {
                var categoryExists = await _taxonomyReader.CategoryExistsAsync(categoryCode, cancellationToken);

                if (!categoryExists)
                {
                    throw new InvalidOperationException(
                        $"Category '{plan.CategoryCode}' does not exist.");
                }
            }

            var subCategoryCode = Normalize(plan.SubCategoryCode);

            if (!string.IsNullOrWhiteSpace(plan.SubCategoryCode))
            {

                var exists =
                    await _taxonomyReader.SubCategoryExistsAsync(plan.SubCategoryCode,cancellationToken);

                if (!exists)
                {
                    throw new InvalidOperationException(
                        $"Subcategory '{plan.SubCategoryCode}' does not belong to " +
                        $"category '{plan.CategoryCode}'.");
                }
            }

            var productTypeCode = Normalize(plan.ProductTypeCode);
            if (!string.IsNullOrWhiteSpace(plan.ProductTypeCode))
            {
                if (string.IsNullOrWhiteSpace(plan.SubCategoryCode))
                {
                    throw new InvalidOperationException(
                        "ProductTypeCode requires SubCategoryCode.");
                }

                var exists =
                    await _taxonomyReader.ProductTypeExistsAsync(plan.ProductTypeCode,cancellationToken);

                if (!exists)
                {
                    throw new InvalidOperationException(
                        $"Product type '{plan.ProductTypeCode}' does not belong to " +
                        $"subcategory '{plan.SubCategoryCode}'.");
                }
            }

            var brand = Normalize(plan.Brand);

            var source = Normalize(plan.Source);


            // -----------------------------
            // 6. Return validated plan
            // -----------------------------

            var validatedPlan = new SearchQueryPlan
            {
                SearchText = searchText,

                CategoryCode = categoryCode,

                SubCategoryCode = subCategoryCode,

                ProductTypeCode = productTypeCode,

                Brand = brand,

                MinPrice = plan.MinPrice,

                MaxPrice = plan.MaxPrice,

                Currency = currency,

                Source = source,

                SortBy = sortBy,

                Descending = plan.Descending,

                Page = page,

                PageSize = pageSize
            };

            return validatedPlan;
        }

        private static string? Normalize(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            return value.Trim();
        }
    }
}
