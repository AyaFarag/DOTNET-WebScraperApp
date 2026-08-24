using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;

namespace Processing.Application.Pipeline
{
    public sealed class CurrencyNormalizationStep : IProcessingStep
    {
        public int Order => 40;

        private static readonly Dictionary<string, string>
            CurrencyMap =
                new(StringComparer.OrdinalIgnoreCase)
                {
                    ["AED"] = "AED",
                    ["د.إ"] = "AED",
                    ["دإ"] = "AED",

                    ["USD"] = "USD",
                    ["$"] = "USD",

                    ["EUR"] = "EUR",
                    ["€"] = "EUR",

                    ["GBP"] = "GBP",
                    ["£"] = "GBP",

                    ["SAR"] = "SAR",
                    ["ر.س"] = "SAR",

                    ["EGP"] = "EGP",
                    ["جنيه"] = "EGP"
                };

        public string Name =>"CurrencyNormalization";

        public Task ExecuteAsync(ProcessingContext context,CancellationToken cancellationToken)
        {
            var currency = context.Input.Currency?.Trim();

            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new InvalidOperationException(
                    "Currency is missing.");
            }

            if (!CurrencyMap.TryGetValue(currency, out var normalizedCurrency))
            {
                throw new InvalidOperationException(
                    $"Unsupported currency: {currency}");
            }

            context.ProcessedPrice.SetNormalizedCurrency(normalizedCurrency);

            return Task.CompletedTask;
        }
    }
}
