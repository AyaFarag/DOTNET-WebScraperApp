using MediatR;
using Source.Application.DTOs;
using Source.Application.Interfaces.Service;

namespace Source.Application.CQRS.Queries.Handler
{
    public class GetAllSourcesQueryHandler : IRequestHandler<GetAllSourcesQuery, IEnumerable<SourceDTO>>
    {
        private readonly ISourceService _sourceService;
        public GetAllSourcesQueryHandler(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        public async Task<IEnumerable<SourceDTO>> Handle(GetAllSourcesQuery request, CancellationToken cancellationToken)
        {
            return await _sourceService.GetAllSourcesAsync();
        }
    }
}
