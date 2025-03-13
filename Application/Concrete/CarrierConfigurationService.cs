using Domain;
using Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{

    public class CarrierConfigurationService
    {
        private readonly IRepository<CarrierConfiguration> _carrierConfigurationRepository;

        public CarrierConfigurationService(IRepository<CarrierConfiguration> carrierConfigurationRepository)
        {
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        public async Task<IEnumerable<CarrierConfiguration>> GetAllCarriersAsync()
        {
            return await _carrierConfigurationRepository.GetAllAsync();
        }

        public async Task<CarrierConfiguration?> GetCarrierConfigurationByIdAsync(int id)
        {
            return await _carrierConfigurationRepository.GetByIdAsync(id);
        }

        public async Task<CarrierConfiguration> AddCarrierConfigurationAsync(CarrierConfiguration carrierConfiguration)
        {
            await _carrierConfigurationRepository.AddAsync(carrierConfiguration);
            return carrierConfiguration;
        }

        public async Task<CarrierConfiguration> UpdateCarrierConfigurationAsync(CarrierConfiguration carrierConfiguration)
        {
            await _carrierConfigurationRepository.UpdateAsync(carrierConfiguration);
            return carrierConfiguration;
        }

        public async Task<bool> DeleteCarrierConfigurationAsync(int id)
        {
            await _carrierConfigurationRepository.DeleteAsync(id);
            return true;
        }
    }
}
