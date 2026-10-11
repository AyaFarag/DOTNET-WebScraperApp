namespace Analytics.Application.DTOs
{
    public sealed record CpiWeightSetValidationResult
    {
        public DateTime Date { get; init; }

        public int WeightCount { get; init; }

        public decimal TotalWeight { get; init; }

        public decimal TotalWeightPercentage { get; init; }

        public bool IsValid { get; init; }

        public string? ErrorMessage { get; init; }
    }
}
