using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Carriers.Command
{
    public class DeleteCarrierCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteCarrierCommand(int id)
        {
            Id = id;
        }
    }
}
