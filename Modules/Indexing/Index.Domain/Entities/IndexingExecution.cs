using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Domain.Entities
{
    public sealed class IndexingExecution
    {
        public Guid Id { get; private set; }

        public Guid BatchId { get; private set; }

        public DateTime StartedAt { get; private set; }

        public DateTime? CompletedAt { get; private set; }

        public string Status { get; private set; } = null!;

        public int TotalProducts { get; private set; }

        public int IndexedProducts { get; private set; }

        public int FailedProducts { get; private set; }

        public string? ErrorMessage { get; private set; }

        private IndexingExecution()
        {
        }

        public IndexingExecution(
            Guid batchId,
            int totalProducts)
        {
            Id = Guid.NewGuid();

            BatchId = batchId;

            StartedAt = DateTime.UtcNow;

            Status = "Running";

            TotalProducts = totalProducts;
        }

        public void IncrementIndexed()
        {
            IndexedProducts++;
        }

        public void IncrementFailed()
        {
            FailedProducts++;
        }

        public void Complete()
        {
            Status = "Completed";

            CompletedAt = DateTime.UtcNow;
        }

        public void Fail(string errorMessage)
        {
            Status = "Failed";

            ErrorMessage = errorMessage;

            CompletedAt = DateTime.UtcNow;
        }
    }
}
