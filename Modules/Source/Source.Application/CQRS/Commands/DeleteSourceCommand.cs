using MediatR;

namespace Source.Application.CQRS.Commands
{
    public record DeleteSourceCommand(Guid id) : IRequest<Unit>
    {
    }
}
