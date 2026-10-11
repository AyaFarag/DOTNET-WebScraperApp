namespace Analytics.Application.DTOs
{
    public sealed record PpiWeightDto
    {
        public Guid Id { get; init; }

        public string CategoryCode { get; init; } = string.Empty;

        public string? CategoryName { get; init; }

        public decimal Weight { get; init; }

        public decimal WeightPercentage { get; init; }

        public DateTime EffectiveFrom { get; init; }

        public DateTime? EffectiveTo { get; init; }
    }
}
