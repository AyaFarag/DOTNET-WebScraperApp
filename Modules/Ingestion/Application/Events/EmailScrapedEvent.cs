using MediatR;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingestion.Application.Events
{
    public class EmailScrapedEvent : BaseEvent
    {
       public string Email { get; set; } = "";
    }
}
