using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarrierConfigurations.Command
{
    public class CreateCarrierConfigurationCommand : IRequest<int>
    {
        public int CarrierId { get; set; }
        public int CarrierMinDesi { get; set; }
        public int CarrierMaxDesi { get; set; }
        public decimal CarrierCost { get; set; }
    }
}
