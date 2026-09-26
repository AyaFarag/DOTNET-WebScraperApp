using MediatR;

namespace Source.Application.CQRS.Commands
{
    public record DisableSourceCommand(Guid id) : IRequest<bool>
    {
    }
}
