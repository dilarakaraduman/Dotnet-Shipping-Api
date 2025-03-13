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
    public class DeleteCarrierCommandHandler : IRequestHandler<DeleteCarrierCommand, int>
    {
        private readonly IRepository<Carrier> _carrierRepository;

        public DeleteCarrierCommandHandler(IRepository<Carrier> carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<int> Handle(DeleteCarrierCommand request, CancellationToken cancellationToken)
        {
            await _carrierRepository.DeleteAsync(request.Id);
            return request.Id;
        }
    }
}
