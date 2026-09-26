using MediatR;
using Source.Application.DTOs;

namespace Source.Application.CQRS.Queries
{
    public record GetAllSourcesQuery : IRequest<IEnumerable<SourceDTO>>
    {
    }
}
