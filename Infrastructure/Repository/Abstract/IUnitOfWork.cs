using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Abstract
{
    // Unit of Work Interface
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Carrier> Carriers { get; }
        IRepository<Order> Orders { get; }
        IRepository<CarrierConfiguration> CarrierConfigurations { get; }
        Task<int> CompleteAsync();
    }
}
