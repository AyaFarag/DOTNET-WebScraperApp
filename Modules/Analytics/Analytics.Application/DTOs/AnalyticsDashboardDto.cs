namespace Analytics.Application.DTOs
{
    public sealed record AnalyticsDashboardDto
    {
        public int TotalProducts { get; init; }
        public int TotalSources { get; init; }

        public decimal AverageMarketPrice { get; init; }

        public int TotalAnomalies { get; init; }
        public int CriticalAnomalies { get; init; }
        public int HighPriceAnomalies { get; init; }
        public int LowPriceAnomalies { get; init; }

        public IReadOnlyCollection<PriceChangeSummaryDto> TopPriceIncreases { get; init; }
            = [];

        public IReadOnlyCollection<PriceChangeSummaryDto> TopPriceDecreases { get; init; }
            = [];
    }

    public sealed record AnalyticsDashboardData
    {
        public int TotalProducts { get; init; }

        public int TotalSources { get; init; }

        public decimal AverageMarketPrice { get; init; }

        public int TotalAnomalies { get; init; }

        public int CriticalAnomalies { get; init; }

        public int HighPriceAnomalies { get; init; }

        public int LowPriceAnomalies { get; init; }

        public IReadOnlyCollection<PriceChangeSummaryDto> TopPriceIncreases { get; init; }
            = [];

        public IReadOnlyCollection<PriceChangeSummaryDto> TopPriceDecreases { get; init; }
            = [];
    }
}
