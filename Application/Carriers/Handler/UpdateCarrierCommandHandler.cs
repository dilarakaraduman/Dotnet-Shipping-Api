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
    public class UpdateCarrierCommandHandler : IRequestHandler<UpdateCarrierCommand, int>
    {
        private readonly IRepository<Carrier> _carrierRepository;

        public UpdateCarrierCommandHandler(IRepository<Carrier> carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<int> Handle(UpdateCarrierCommand request, CancellationToken cancellationToken)
        {
            var carrier = await _carrierRepository.GetByIdAsync(request.Id);
            if (carrier == null)
                return 0;

            carrier.CarrierName = request.Name;
            carrier.CarrierPlusDesiCost = request.CarrierPlusDesiCost;

            await _carrierRepository.UpdateAsync(carrier);
            return carrier.CarrierId;
        }
    }
}
