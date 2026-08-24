using Application.Interfaces;
using Ingestion.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Comand
{
    public class ScrapePricesCommandHandler : IRequestHandler<ScrapePricesCommand, string>
    {
    private readonly IJobScheduler _jobScheduler;

    public ScrapePricesCommandHandler(IJobScheduler jobScheduler)
    {
        _jobScheduler = jobScheduler;
    }

    public async Task<string> Handle(ScrapePricesCommand request,CancellationToken cancellationToken)
    {
        var jobId = _jobScheduler.EnqueueScraping();

        return jobId;
    }
}
}
