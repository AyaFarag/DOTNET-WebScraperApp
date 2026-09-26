using MediatR;

namespace Source.Application.CQRS.Commands
{
    public record EnableSourceCommand(Guid id) : IRequest<bool>
    {
    }
}
