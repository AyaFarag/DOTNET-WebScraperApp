namespace Analytics.Application.DTOs
{
    public sealed record CategoryIndexDto
    {
        public string CategoryCode { get; init; } = string.Empty;

        public string Currency { get; init; } = string.Empty;

        public decimal BaseWeightedPrice { get; init; }

        public decimal CurrentWeightedPrice { get; init; }

        public decimal IndexValue { get; init; }

        public decimal? ChangePercentage { get; init; }

        public DateTime BaseDate { get; init; }

        public DateTime CurrentDate { get; init; }
    }
}
