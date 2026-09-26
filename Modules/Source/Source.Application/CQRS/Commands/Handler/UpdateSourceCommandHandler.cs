using MediatR;
using Source.Application.Interfaces.Service;

namespace Source.Application.CQRS.Commands.Handler
{
    public class UpdateSourceCommandHandler : IRequestHandler<UpdateSourceCommand, Unit>
    {
        private readonly ISourceService _sourceService;
        public UpdateSourceCommandHandler(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        public async Task<Unit> Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
        {
            await _sourceService.UpdateSourceAsync(request.id, request.updateSource, cancellationToken);
            return Unit.Value;
        }
    }
}
