using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;

namespace Processing.Application.Pipeline
{
    public sealed class UnitNormalizationStep : IProcessingStep
    {
        public int Order => 60;
        public string Name => "UnitNormalization";

        public Task ExecuteAsync(ProcessingContext context, CancellationToken cancellationToken)
        {
            var quantity = context.Input.Quantity;

            var unit = context.Input.Unit?.Trim();

            if (!quantity.HasValue || string.IsNullOrWhiteSpace(unit))
            {
                return Task.CompletedTask;
            }

            switch (unit.ToLowerInvariant())
            {
                case "ml":
                case "milliliter":
                case "milliliters":

                    context.ProcessedPrice.SetNormalizedQuantity(quantity.Value / 1000m);

                    context.ProcessedPrice.SetNormalizedUnit("L");

                    break;

                case "l":
                case "liter":
                case "liters":

                    context.ProcessedPrice.SetNormalizedQuantity(quantity.Value);

                    context.ProcessedPrice.SetNormalizedUnit("L");

                    break;

                case "g":
                case "gram":
                case "grams":

                    context.ProcessedPrice.SetNormalizedQuantity(quantity.Value / 1000m);

                    context.ProcessedPrice.SetNormalizedUnit("KG");

                    break;

                case "kg":
                case "kilogram":
                case "kilograms":

                    context.ProcessedPrice.SetNormalizedQuantity(quantity.Value);

                    context.ProcessedPrice.SetNormalizedUnit("KG");

                    break;

                case "pc":
                case "pcs":
                case "piece":
                case "pieces":

                    context.ProcessedPrice.SetNormalizedQuantity(quantity.Value);

                    context.ProcessedPrice.SetNormalizedUnit("PCS");

                    break;

                default:

                    throw new InvalidOperationException($"Unsupported unit: {unit}");
            }

            context.ProcessedPrice.SetUnit(unit);

            return Task.CompletedTask;
        }
    }
}
