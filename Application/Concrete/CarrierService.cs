using Domain;
using Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Concrete
{
    // Service Layer
    public class CarrierService
    {
        private readonly IRepository<Carrier> _carrierRepository;

        public CarrierService(IRepository<Carrier> carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<IEnumerable<Carrier>> GetAllCarriersAsync()
        {
            return await _carrierRepository.GetAllAsync();
        }

        public async Task<Carrier?> GetCarrierByIdAsync(int id)
        {
            return await _carrierRepository.GetByIdAsync(id);
        }

        public async Task<Carrier> AddCarrierAsync(Carrier carrier)
        {
            await _carrierRepository.AddAsync(carrier);
            return carrier;
        }

        public async Task<Carrier> UpdateCarrierAsync(Carrier carrier)
        {
            await _carrierRepository.UpdateAsync(carrier);
            return carrier;
        }

        public async Task<bool> DeleteCarrierAsync(int id)
        {
            await _carrierRepository.DeleteAsync(id);
            return true;
        }
    }
}
