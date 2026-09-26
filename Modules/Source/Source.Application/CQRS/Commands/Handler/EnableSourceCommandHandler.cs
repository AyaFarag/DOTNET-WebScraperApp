using MediatR;
using Source.Application.Interfaces.Service;

namespace Source.Application.CQRS.Commands.Handler
{
    public class EnableSourceCommandHandler : IRequestHandler<EnableSourceCommand, bool>
    {
        private readonly ISourceService _sourceService;
        public EnableSourceCommandHandler(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        public async Task<bool> Handle(EnableSourceCommand request, CancellationToken cancellationToken)
        {
            await _sourceService.EnableSource(request.id, cancellationToken);
            return true;
        }
    }
}
