using Ingestion.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.CQRS.Query
{
    public class ScrapePricesQuery : IRequest<HashSet<string>>
    {
    }
}
