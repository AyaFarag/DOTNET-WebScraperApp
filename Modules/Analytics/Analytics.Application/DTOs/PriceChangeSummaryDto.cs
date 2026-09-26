namespace Analytics.Application.DTOs
{
    public sealed record PriceChangeSummaryDto
    {
        public string ProductKey { get; init; } = string.Empty;
        public string? ProductName { get; init; }
        public decimal CurrentPrice { get; init; }
        public decimal PreviousPrice { get; init; }
        public decimal ChangePercentage { get; init; }
        public string? Source { get; init; }
        public string Currency { get; init; } = string.Empty;
    }
}
