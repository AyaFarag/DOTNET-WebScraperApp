namespace Analytics.Domain.Entities
{
    public sealed class AnomalyExplanation
    {
        public Guid Id { get; private set; }

        public Guid AnomalyId { get; private set; }

        public string Summary { get; private set; } = string.Empty;

        public string Explanation { get; private set; } = string.Empty;

        public string? PossibleInterpretation { get; private set; }

        public string Confidence { get; private set; } = string.Empty;

        public string Model { get; private set; } = string.Empty;

        public string PromptVersion { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; private set; }

        private AnomalyExplanation()
        {
        }

        public AnomalyExplanation(
            Guid anomalyId,
            string summary,
            string explanation,
            string? possibleInterpretation,
            string confidence,
            string model,
            string promptVersion)
        {
            Id = Guid.NewGuid();
            AnomalyId = anomalyId;
            Summary = summary;
            Explanation = explanation;
            PossibleInterpretation = possibleInterpretation;
            Confidence = confidence;
            Model = model;
            PromptVersion = promptVersion;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
