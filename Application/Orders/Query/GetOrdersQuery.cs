using MediatR;

namespace Application.Orders.Query
{
    public class GetOrdersQuery : IRequest<IEnumerable<Domain.Order>>
    {
    }
}
