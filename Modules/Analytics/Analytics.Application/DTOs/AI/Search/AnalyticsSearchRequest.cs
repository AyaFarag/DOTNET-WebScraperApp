namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record AnalyticsSearchRequest
    {
        public string Query { get; init; } = string.Empty;

        public int Page { get; init; } = 1;

        public int PageSize { get; init; } = 20;
    }
}
