namespace Analytics.Application.DTOs.AI.AnomalyExplanation
{
    public sealed record AnomalyExplanationResult
    {
        public string Summary { get; init; } = string.Empty;

        public string Explanation { get; init; } = string.Empty;

        public string? PossibleInterpretation { get; init; }

        public IReadOnlyCollection<string> KeyFactors { get; init; }
            = [];

        public string Confidence { get; init; } = string.Empty;
    }
}
