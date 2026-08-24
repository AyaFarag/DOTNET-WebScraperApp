using MediatR;

namespace Application.CQRS.Comand
{
    public class ScrapePricesCommand : IRequest<string>
    {
    }
}
