using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public abstract class BaseEvent : INotification
    {
        public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
    }
}
