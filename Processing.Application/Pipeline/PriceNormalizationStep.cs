using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;

namespace Processing.Application.Pipeline
{
    public sealed class PriceNormalizationStep : IProcessingStep
    {
        public int Order => 30;
        public string Name => "PriceNormalization";

        public Task ExecuteAsync(ProcessingContext context, CancellationToken cancellationToken)
        {
            var price = context.Input.Price;

            if (price < 0)
            {
                throw new InvalidOperationException(
                    "Price cannot be negative.");
            }

            // Standardize precision
            price = Math.Round(price,4, MidpointRounding.AwayFromZero);

            context.ProcessedPrice.SetNormalizedPrice(price);

            return Task.CompletedTask;
        }
    }
}
