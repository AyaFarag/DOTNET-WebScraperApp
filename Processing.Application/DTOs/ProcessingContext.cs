using Processing.Domain.Entities;
using Shared.Domain;

namespace Processing.Application.DTOs
{
    public sealed class ProcessingContext
    {
        public RawPriceData Input { get; }
        public RawPrice RawPrice { get; }

        public ProcessedPrice ProcessedPrice { get; }

        public ProcessingContext(RawPrice rawPrice, ProcessedPrice processedPrice)
        {
            RawPrice = rawPrice;
            ProcessedPrice = processedPrice;
        }
    }
}
