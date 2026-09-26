using Index.Application.DTOs;
using Index.Domain.Entities;

namespace Index.Application.Mapper
{
    public static class IndexedProductSearchMapper
    {
        public static ProductSearchDocument Map(IndexedProduct product)
        {
            return new ProductSearchDocument
            {
                Id = product.Id.ToString(),

                ProductKey = product.ProductKey,

                ProductName = product.ProductName,

                Brand = product.Brand,

                CategoryCode = product.CategoryCode,

                SubCategoryCode = product.SubCategoryCode,

                ProductGroupCode = product.ProductGroupCode,

                ProductTypeCode = product.ProductTypeCode,

                Price = (double)product.Price,

                Currency = product.Currency,

                UnitPrice = product.UnitPrice.HasValue ? (double)product.UnitPrice.Value : null,

                Unit = product.Unit,

                Source = product.Source,

                CollectedAt = product.CollectedAt
            };
        }
    }
}
