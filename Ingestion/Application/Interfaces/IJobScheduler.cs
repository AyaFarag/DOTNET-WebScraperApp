using Ingestion.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJobScheduler
    {
        string EnqueueScraping();

        string ScheduleScraping(
            DateTimeOffset executeAt);

        void ScheduleRecurringScraping(
            string jobId,
            string cronExpression);
    }
}
