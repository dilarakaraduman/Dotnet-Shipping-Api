using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Command
{
    public class DeleteOrderCommand : IRequest<int>
    {
        public int OrderId { get; set; }

        public DeleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
