using Processing.Application.DTOs;
using Processing.Application.Interfaces.Services;

namespace Processing.Application.Pipeline
{
    public sealed class CleaningStep : IProcessingStep
    {

        public string Name => "Cleaning";

        public int Order => 0;

        public Task ExecuteAsync(ProcessingContext context,CancellationToken cancellationToken)
        {
            var cleanedName = context.ProcessedPrice.ProductName.Trim();

            cleanedName = string.Join(" ", cleanedName.Split(' ', StringSplitOptions.RemoveEmptyEntries));

            return Task.CompletedTask;
        }
    }
}
