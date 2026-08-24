using MediatR;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.Events
{
    public class PriceScrapedEvent : BaseEvent
    {
       public string ProductName { get; set; } = "";
       public string RawPrice { get; set; } = "";
       public string Source { get; set; } = "";
    }
}
