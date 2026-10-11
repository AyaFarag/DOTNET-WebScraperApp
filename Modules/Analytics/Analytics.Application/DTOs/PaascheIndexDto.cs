namespace Analytics.Application.DTOs
{
    public sealed record PaascheIndexDto
    {
        public string CategoryCode { get; init; } = string.Empty;

        public string Currency { get; init; } = string.Empty;

        public decimal BaseBasketValue { get; init; }

        public decimal CurrentBasketValue { get; init; }

        public decimal IndexValue { get; init; }

        public decimal? ChangePercentage { get; init; }

        public DateTime BaseDate { get; init; }

        public DateTime CurrentDate { get; init; }
    }
}
