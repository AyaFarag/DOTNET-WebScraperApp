using Analytics.Application.Enums;
using Analytics.Domain.Enums;

namespace Analytics.Application.DTOs.AI.Search
{
    public sealed record AnalyticsSearchQueryPlan
    {
        public AnalyticsSearchIntent Intent { get; init; }
        public DateRangeType rangeType { get; init; }

        public string? ProductKey { get; init; }

        public string? ProductName { get; init; }

        public string? CategoryCode { get; init; }

        public string? Source { get; init; }

        public string? Currency { get; init; }

        public DateTime? From { get; init; }

        public DateTime? To { get; init; }

        public int? Limit { get; init; }

        public string? SortBy { get; init; }

        public string? SortDirection { get; init; }
    }
}
