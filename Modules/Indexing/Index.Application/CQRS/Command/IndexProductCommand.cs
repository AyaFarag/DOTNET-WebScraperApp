using Index.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Index.Application.CQRS.Command
{
    public sealed record IndexProductCommand(
    Guid EventId,
    Guid IndexedProductId);
}
