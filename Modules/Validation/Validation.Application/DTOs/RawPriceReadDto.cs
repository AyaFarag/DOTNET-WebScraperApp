namespace Validation.Application.DTOs
{
    public sealed class RawPriceReadDto
    {
        public Guid Id { get; init; }

        public Guid BatchId { get; init; }

        public string Source { get; init; } = null!;

        public string SourceUrl { get; init; } = null!;

        public string ProductName { get; init; } = null!;

        public string RawPriceValue { get; init; } = null!;

        public string? Currency { get; init; }

        public DateTime CollectedAt { get; init; }

        public string? RawData { get; init; }
    }
}
