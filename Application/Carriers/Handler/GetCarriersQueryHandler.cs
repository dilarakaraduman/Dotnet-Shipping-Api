using Application.Carriers.Query;
using Application.Concrete;
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
    public class GetCarriersQueryHandler : IRequestHandler<GetCarriersQuery, List<Carrier>>
    {
        private readonly IRepository<Carrier> _carrierRepository;
        private readonly CarrierService _carrierService;

        public GetCarriersQueryHandler(IRepository<Carrier> carrierRepository, CarrierService carrierService)
        {
            _carrierRepository = carrierRepository;
            _carrierService = carrierService;
        }

        public async Task<List<Carrier>> Handle(GetCarriersQuery request, CancellationToken cancellationToken)
        {
            return (await _carrierService.GetAllCarriersAsync()).ToList();
        }


    }
}
