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
    public class CreateCarrierConfigurationCommandHandler : IRequestHandler<CreateCarrierConfigurationCommand, int>
    {
        private readonly IRepository<CarrierConfiguration> _carrierConfigurationRepository;

        public CreateCarrierConfigurationCommandHandler(IRepository<CarrierConfiguration> carrierConfigurationRepository)
        {
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        public async Task<int> Handle(CreateCarrierConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = new CarrierConfiguration
            {
                CarrierId = request.CarrierId,
                CarrierMinDesi = request.CarrierMinDesi,
                CarrierMaxDesi = request.CarrierMaxDesi,
                CarrierCost = request.CarrierCost
            };

            await _carrierConfigurationRepository.AddAsync(configuration);
            return configuration.CarrierConfigurationId;
        }
    }
}
