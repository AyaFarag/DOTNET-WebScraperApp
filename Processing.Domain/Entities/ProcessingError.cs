using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Domain.Entities
{
    public sealed class ProcessingError
    {
        public Guid Id { get; private set; }

        public Guid ProcessedPriceId { get; private set; }

        public string Step { get; private set; } = null!;

        public string Message { get; private set; } = null!;

        public DateTime CreatedAtUtc { get; private set; }

        public ProcessedPrice ProcessedPrice { get; private set; } = null!;

        private ProcessingError()
        {
        }

        public ProcessingError(
            Guid processedPriceId,
            string step,
            string message)
        {
            Id = Guid.NewGuid();
            ProcessedPriceId = processedPriceId;
            Step = step;
            Message = message;
            CreatedAtUtc = DateTime.UtcNow;
        }
    }
}
