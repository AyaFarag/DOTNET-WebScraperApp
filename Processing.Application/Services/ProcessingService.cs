using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;
using Processing.Domain.Entities;
using Shared.Domain;

namespace Processing.Application.Services
{
    public sealed class ProcessingService : IProcessingService
    {
        private readonly IEnumerable<IProcessingStep> _steps;
        public ProcessingService(IEnumerable<IProcessingStep> steps)
        {
            _steps = steps;
        }

        public async Task<ProcessingBatchResult> ProcessAsync(Guid batchId, IReadOnlyCollection<RawPrice> rawPrices,
            CancellationToken cancellationToken)
        {
            var __steps = _steps.OrderBy(x => x.Order).ToList(); 

            var results = new List<ProcessedPrice>();
            var errors = new List<ProcessingErrorResult>();

            foreach (var rawPrice in rawPrices)
            {
                var processedPrice = new ProcessedPrice(
                        batchId,
                        rawPrice.Id,
                        rawPrice.ProductName,
                        decimal.Parse(rawPrice.RawPriceValue.ToString()),
                        rawPrice.Currency,
                        rawPrice.Source);

                var context = new ProcessingContext(rawPrice, processedPrice);
                var failed = false;

                foreach (var step in __steps)
                {
                    try
                    {
                        await step.ExecuteAsync(context, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(new ProcessingErrorResult(rawPrice.Id, step.Name, ex.Message));
                        failed = true; // Stop processing this raw price if a step fails
                        continue;
                    }
                }

                if (!failed)
                {
                    results.Add(processedPrice);
                }
            }

            // totalCount // processedCount  // failedCount   // errors
            return new ProcessingBatchResult(batchId, rawPrices.Count, results.Count, errors.Count, results, errors);
        }
    }
}
