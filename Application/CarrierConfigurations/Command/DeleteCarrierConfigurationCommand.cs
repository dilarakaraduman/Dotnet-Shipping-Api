using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarrierConfigurations.Command
{
    public class DeleteCarrierConfigurationCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteCarrierConfigurationCommand(int id)
        {
            Id = id;
        }
    }
}
