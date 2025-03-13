using Application.Carriers.Command;
using Domain;
using Infrastructure.Repository.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Carriers.Handler
{
    public class CreateCarrierCommandHandler : IRequestHandler<CreateCarrierCommand, int>
    {
        private readonly IRepository<Carrier> _carrierRepository;

        public CreateCarrierCommandHandler(IRepository<Carrier> carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<int> Handle(CreateCarrierCommand request, CancellationToken cancellationToken)
        {
            var carrier = new Carrier
            {
                CarrierName = request.Name,
                CarrierPlusDesiCost = request.CarrierPlusDesiCost
            };

            await _carrierRepository.AddAsync(carrier);
            return carrier.CarrierId;
        }
    }
}
