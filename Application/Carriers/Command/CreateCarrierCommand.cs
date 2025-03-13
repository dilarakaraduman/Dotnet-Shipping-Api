using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Carriers.Command
{
    public class CreateCarrierCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal CarrierPlusDesiCost { get; set; }
    }
}
