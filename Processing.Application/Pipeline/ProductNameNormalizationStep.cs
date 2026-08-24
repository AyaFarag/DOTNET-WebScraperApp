using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;

namespace Processing.Application.Pipeline
{
    public sealed class ProductNameNormalizationStep : IProcessingStep
    {
        public int Order => 10;
        public string Name => "ProductNameNormalization";

        public Task ExecuteAsync(ProcessingContext context, CancellationToken cancellationToken)
        {
            var productName = context.Input.ProductName;

            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new InvalidOperationException(
                    "Product name is empty.");
            }

            // Remove leading/trailing spaces
            productName = productName.Trim();

            // Normalize multiple spaces
            productName = string.Join(" ", productName.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            // Normalize common separators
            productName = productName
                .Replace("–", "-")
                .Replace("—", "-");

            context.ProcessedPrice.SetNormalizedProductName(productName);

            return Task.CompletedTask;
        }
    }
}
