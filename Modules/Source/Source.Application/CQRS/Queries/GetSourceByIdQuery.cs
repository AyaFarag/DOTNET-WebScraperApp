using MediatR;
using Source.Application.DTOs;

namespace Source.Application.CQRS.Queries
{
    public record GetSourceByIdQuery(Guid id) : IRequest<SourceDTO>
    {
    }
}
