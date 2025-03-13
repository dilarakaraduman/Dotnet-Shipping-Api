using Application.CarrierConfigurations.Query;
using Application.Concrete;
using Domain;
using Infrastructure.Repository.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CarrierConfigurations.Handler
{
    public class GetCarrierConfigurationsQueryHandler : IRequestHandler<GetCarrierConfigurationsQuery, List<CarrierConfiguration>>
    {
        private readonly IRepository<CarrierConfiguration> _carrierConfigurationRepository;
        private readonly CarrierConfigurationService _carrierConfigurationService;

        public GetCarrierConfigurationsQueryHandler(IRepository<CarrierConfiguration> carrierConfigurationRepository, CarrierConfigurationService carrierConfigurationService)
        {
            _carrierConfigurationRepository = carrierConfigurationRepository;
            _carrierConfigurationService = carrierConfigurationService;
        }

        public async Task<List<CarrierConfiguration>> Handle(GetCarrierConfigurationsQuery request, CancellationToken cancellationToken)
        {
            return (await _carrierConfigurationService.GetAllCarriersAsync()).ToList();
        }
    }
}

