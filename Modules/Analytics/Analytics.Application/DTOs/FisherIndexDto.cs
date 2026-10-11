namespace Analytics.Application.DTOs
{
    public sealed record FisherIndexDto
    {
        public string CategoryCode { get; init; } = string.Empty;

        public string Currency { get; init; } = string.Empty;

        public decimal LaspeyresValue { get; init; }

        public decimal PaascheValue { get; init; }

        public decimal IndexValue { get; init; }

        public decimal? ChangePercentage { get; init; }

        public DateTime BaseDate { get; init; }

        public DateTime CurrentDate { get; init; }
    }
}
