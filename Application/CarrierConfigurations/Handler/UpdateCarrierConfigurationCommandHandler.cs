using Application.CarrierConfigurations.Command;
using Domain;
using Infrastructure.Repository.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarrierConfigurations.Handler
{
    public class UpdateCarrierConfigurationCommandHandler : IRequestHandler<UpdateCarrierConfigurationCommand, int>
    {
        private readonly IRepository<CarrierConfiguration> _carrierConfigurationRepository;

        public UpdateCarrierConfigurationCommandHandler(IRepository<CarrierConfiguration> carrierConfigurationRepository)
        {
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        public async Task<int> Handle(UpdateCarrierConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = await _carrierConfigurationRepository.GetByIdAsync(request.Id);
            if (configuration == null)
                return 0;

            configuration.CarrierId = request.CarrierId;
            configuration.CarrierMinDesi = request.CarrierMinDesi;
            configuration.CarrierMaxDesi = request.CarrierMaxDesi;
            configuration.CarrierCost = request.CarrierCost;

            await _carrierConfigurationRepository.UpdateAsync(configuration);
            return configuration.CarrierConfigurationId;
        }
    }
}
