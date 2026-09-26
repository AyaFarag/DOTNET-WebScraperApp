namespace Index.Application.DTOs.Search
{
    public sealed class ProductSearchResultDto
    {
        public string Id { get; init; } = null!;

        public string ProductKey { get; init; } = null!;

        public string ProductName { get; init; } = null!;

        public string? Brand { get; init; }

        public string CategoryCode { get; init; } = null!;

        public string SubCategoryCode { get; init; } = null!;

        public string ProductTypeCode { get; init; } = null!;

        public double Price { get; init; }

        public string Currency { get; init; } = null!;

        public double? UnitPrice { get; init; }

        public string? Unit { get; init; }

        public string Source { get; init; } = null!;

        public DateTimeOffset CollectedAt { get; init; }
    }
}
