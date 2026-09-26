using MediatR;
using Source.Application.DTOs;

namespace Source.Application.CQRS.Commands
{
    public record CreateSourceCommand(SourceRequest sourceRequest) : IRequest<Unit>
    {

    }
}
