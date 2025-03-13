using Application.Orders.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Command
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int OrderDesi { get; set; }

        public CreateOrderCommand(int orderDesi)
        {
            OrderDesi = orderDesi;
        }
    }
}
