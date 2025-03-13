using Domain;
using Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Concrete
{
    // Unit of Work Implementation
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShippingDbContext _context;
        public IRepository<Carrier> Carriers { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<CarrierConfiguration> CarrierConfigurations { get; }

        public UnitOfWork(ShippingDbContext context)
        {
            _context = context;
            Carriers = new Repository<Carrier>(context);
            Orders = new Repository<Order>(context);
            CarrierConfigurations = new Repository<CarrierConfiguration>(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
