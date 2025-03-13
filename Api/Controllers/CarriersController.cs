using Application.Carriers.Command;
using Application.Carriers.Query;
using Application.Concrete;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarriersController : ControllerBase
    {
        private readonly CarrierService _carrierService;
        private readonly IMediator _mediator;

        public CarriersController(CarrierService carrierService, IMediator mediator)
        {
            _carrierService = carrierService;
            _mediator = mediator;
        }  

        [HttpGet]
        public async Task<IActionResult> GetCarriers()
        {
            var carriers = await _mediator.Send(new GetCarriersQuery());
            return Ok(carriers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarrier([FromBody] CreateCarrierCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok($"{result} ID’li kargo firması başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrier(int id)
        {
            var result = await _mediator.Send(new DeleteCarrierCommand(id));
            return Ok($"{result} ID’li kargo firması başarıyla silindi.");
        }
    }
}
