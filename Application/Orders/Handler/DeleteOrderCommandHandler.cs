using Application.Orders.Command;
using Domain;
using Infrastructure.Repository.Abstract;
using MediatR;


namespace Application.Orders.Handler
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
    {
        private readonly IRepository<Order> _orderRepository;

        public DeleteOrderCommandHandler(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.DeleteAsync(request.OrderId);
            return request.OrderId;
        }
    }
}
