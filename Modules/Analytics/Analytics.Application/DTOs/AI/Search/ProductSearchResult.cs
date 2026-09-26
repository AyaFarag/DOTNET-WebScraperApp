namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record ProductSearchResult(
    IReadOnlyCollection<ProductSearchItemDto> Items,
    int Page,
    int PageSize,
    long TotalCount)
    {
        public int TotalPages =>
            (int)Math.Ceiling(
                TotalCount / (double)PageSize);
    }
}
