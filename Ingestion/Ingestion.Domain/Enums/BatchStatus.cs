using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Domain.Enums
{
    public enum BatchStatus
    {
        Created = 0,

        Queued = 1,

        Scraping = 2,

        IngestionCompleted = 3,

        ValidationProcessing = 4,

        ValidationCompleted = 5,

        Processing = 6,

        ProcessingCompleted = 7,

        ClassificationProcessing = 8,

        ClassificationCompleted = 9,

        IndexingProcessing = 10,

        Completed = 11,

        Failed = 99
    }
}
