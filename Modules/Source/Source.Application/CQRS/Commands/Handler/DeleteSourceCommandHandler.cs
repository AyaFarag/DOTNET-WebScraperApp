using MediatR;
using Source.Application.Interfaces.Service;

namespace Source.Application.CQRS.Commands.Handler
{
    public class DeleteSourceCommandHandler : IRequestHandler<DeleteSourceCommand, Unit>
    {
        private readonly ISourceService _sourceService;
        public DeleteSourceCommandHandler(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        public async Task<Unit> Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
        {
            await _sourceService.DeleteSourceAsync(request.id, cancellationToken);
            return Unit.Value;
        }
    }
}
