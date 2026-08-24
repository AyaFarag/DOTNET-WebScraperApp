using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Comand
{
    public class ScrapePricesCommandHandler : IRequestHandler<ScrapePricesCommand, Unit>
    {
        public Task<Unit> Handle(ScrapePricesCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
