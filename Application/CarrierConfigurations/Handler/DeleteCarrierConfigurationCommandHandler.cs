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
    public class DeleteCarrierConfigurationCommandHandler : IRequestHandler<DeleteCarrierConfigurationCommand, int>
    {
        private readonly IRepository<CarrierConfiguration> _carrierConfigurationRepository;

        public DeleteCarrierConfigurationCommandHandler(IRepository<CarrierConfiguration> carrierConfigurationRepository)
        {
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        public async Task<int> Handle(DeleteCarrierConfigurationCommand request, CancellationToken cancellationToken)
        {
            await _carrierConfigurationRepository.DeleteAsync(request.Id);
            return request.Id;
        }
    }
}
