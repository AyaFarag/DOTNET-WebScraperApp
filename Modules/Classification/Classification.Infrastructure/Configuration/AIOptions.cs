namespace Classification.Infrastructure.Configuration
{
    public sealed class AIOptions
    {
        public const string SectionName = "AI";

        public string Provider { get; set; } = "OpenAI";

        public string Model { get; set; } = null!;

        public string ApiKey { get; set; } = null!;

        public double Temperature { get; set; } = 0;

        public int MaxTokens { get; set; } = 1000;
    }
}
