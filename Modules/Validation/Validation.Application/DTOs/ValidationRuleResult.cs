namespace Validation.Application.DTOs
{
    public sealed class ValidationRuleResult
    {
        public bool IsValid { get; init; }
        public string? RuleName { get; init;  }

        public string? ErrorMessage { get; init; }

        public static ValidationRuleResult Success()
        {
            return new ValidationRuleResult
            {
                IsValid = true
            };
        }

        public static ValidationRuleResult Failure(
            string message)
        {
            return new ValidationRuleResult
            {
                IsValid = false,
                ErrorMessage = message
            };
        }
    }
}
