using MediatR;
using Source.Application.Interfaces.Service;

namespace Source.Application.CQRS.Commands.Handler
{
    public class CreateSourceCommandHandler : IRequestHandler<CreateSourceCommand, Unit>
    {
        private readonly ISourceService _sourceService;
        public CreateSourceCommandHandler(ISourceService sourceService)
        {
            _sourceService = sourceService;

        }
        public async Task<Unit> Handle(CreateSourceCommand request, CancellationToken cancellationToken)
        {
            await _sourceService.AddSourceAsync(request.sourceRequest, cancellationToken);
            return Unit.Value;
        }
    }
}
