using MediatR;
using Source.Application.Interfaces.Service;

namespace Source.Application.CQRS.Commands.Handler
{
    public class DisableSourceCommandHandler : IRequestHandler<DisableSourceCommand, bool>
    {
        public readonly ISourceService _sourceService;


        public DisableSourceCommandHandler(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        public async Task<bool> Handle(DisableSourceCommand request, CancellationToken cancellationToken)
        {
            await _sourceService.DisableSource(request.id, cancellationToken);
            return true;
        }
    }
}
