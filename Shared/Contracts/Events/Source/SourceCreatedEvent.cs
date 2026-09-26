using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Events.Source
{
    public class SourceCreatedEvent : INotification
    {
        public Guid SourceId { get; set; }
    }
}
