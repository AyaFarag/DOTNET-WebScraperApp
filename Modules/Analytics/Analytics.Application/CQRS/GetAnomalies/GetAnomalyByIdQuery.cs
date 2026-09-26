using Analytics.Application.DTOs;
using MediatR;

namespace Analytics.Application.CQRS.GetAnomalies
{
    public sealed record GetAnomalyByIdQuery(Guid Id) : IRequest<PriceAnomalyDto?>;
}
