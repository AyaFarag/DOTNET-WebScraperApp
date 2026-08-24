using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;

namespace Processing.Application.Pipeline
{
    public sealed class UnitPriceCalculationStep : IProcessingStep
    {
        public int Order => 80;
        public string Name => "UnitPriceCalculation";

        public Task ExecuteAsync(ProcessingContext context, CancellationToken cancellationToken)
        {
            var price = context.ProcessedPrice.Price;

            var totalQuantity = context.ProcessedPrice.TotalQuantity;

            if (!totalQuantity.HasValue)
            {
                return Task.CompletedTask;
            }

            if (totalQuantity.Value <= 0)
            {
                throw new InvalidOperationException(
                    "Total quantity must be greater than zero.");
            }

            var unitPrice = price / totalQuantity.Value;

            unitPrice = Math.Round(unitPrice,6, MidpointRounding.AwayFromZero);

            context.ProcessedPrice.SetUnitPrice(unitPrice);

            return Task.CompletedTask;
        }
    }
}
