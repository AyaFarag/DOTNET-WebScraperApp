using MediatR;
using Source.Application.DTOs;
using Source.Application.Interfaces.Service;

namespace Source.Application.CQRS.Queries.Handler
{
    public class GetSourceByIdQueryHandler : IRequestHandler<GetSourceByIdQuery, SourceDTO>
    {
        private readonly ISourceService _sourceService;
        public GetSourceByIdQueryHandler(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }
        public async Task<SourceDTO> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var source = await _sourceService.GetSourceByIdAsync(request.id);
            return source;
        }
    }
}
