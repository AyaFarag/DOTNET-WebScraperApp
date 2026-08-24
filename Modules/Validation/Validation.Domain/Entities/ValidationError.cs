namespace Validation.Domain.Entities
{
    public sealed class ValidationError
    {
        public Guid Id { get; private set; }
        public string Rule { get; private set; } = null!;

        public string Message { get; private set; } = null!;

        public ValidationResult ValidationResult { get; private set; } = null!;
        public Guid ValidationResultId { get; private set; }

        public ValidationError(
            string rule,
            string message)
        {
            Rule = rule;
            Message = message;
        }
    }
}
