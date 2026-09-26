using Analytics.Application.DTOs.AI.Search;
using Analytics.Application.Interfaces.Logic;

namespace Analytics.Application.Services.Logic
{
    public sealed class ProductSearchRequestMapper : IProductSearchRequestMapper
    {
        public ProductSearchRequest Map(ProductSearchQueryPlan plan)
        {
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
                SortBy = plan.SortBy,
                SortDirection = plan.SortDirection,
                Page = plan.Page,
                PageSize = plan.PageSize
            };
        }
    }
}
