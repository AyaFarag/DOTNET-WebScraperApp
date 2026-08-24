using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;

namespace Processing.Application.Pipeline
{
    public sealed class BrandNormalizationStep : IProcessingStep
    {
        public int Order => 20;
        public string Name => "BrandNormalization";

        public Task ExecuteAsync(ProcessingContext context,
            CancellationToken cancellationToken)
        {
            var brand = context.Input.Brand;

            if (string.IsNullOrWhiteSpace(brand))
            {
                return Task.CompletedTask;
            }

            brand = brand.Trim();

            brand = string.Join(" ",brand.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            brand = brand.ToLowerInvariant();

            brand = char.ToUpperInvariant(brand[0]) + brand[1..];

            context.ProcessedPrice.SetBrand(brand);

            return Task.CompletedTask;
        }
    }
}
