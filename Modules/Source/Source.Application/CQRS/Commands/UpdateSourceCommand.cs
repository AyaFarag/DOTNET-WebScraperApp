using MediatR;
using Source.Application.DTOs;

namespace Source.Application.CQRS.Commands
{
    public record UpdateSourceCommand(Guid id, UpdateSourceRequest updateSource) : IRequest<Unit>
    {
    }
}
