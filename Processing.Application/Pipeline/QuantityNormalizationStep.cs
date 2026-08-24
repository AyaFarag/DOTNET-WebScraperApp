using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;

namespace Processing.Application.Pipeline
{
    public sealed class QuantityNormalizationStep : IProcessingStep
    {
        public int Order => 50;
        public string Name => "QuantityNormalization";

        public Task ExecuteAsync(ProcessingContext context, CancellationToken cancellationToken)
        {
            var quantity = context.Input.Quantity;

            if (!quantity.HasValue)
            {
                return Task.CompletedTask;
            }

            if (quantity.Value <= 0)
            {
                throw new InvalidOperationException(
                    "Quantity must be greater than zero.");
            }

            var normalized = Math.Round(quantity.Value,6,MidpointRounding.AwayFromZero);

            context.ProcessedPrice.SetQuantity(quantity);

            context.ProcessedPrice.SetNormalizedQuantity(normalized);

            return Task.CompletedTask;
        }
    }
}
