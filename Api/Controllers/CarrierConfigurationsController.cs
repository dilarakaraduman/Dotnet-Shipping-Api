using Application.CarrierConfigurations.Command;
using Application.CarrierConfigurations.Query;
using Application.Concrete;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationsController : ControllerBase
    {
        private readonly CarrierConfigurationService _carrierConfigurationService;
        private readonly IMediator _mediator;

        public CarrierConfigurationsController(CarrierConfigurationService carrierConfigurationService, IMediator mediator)
        {
            _carrierConfigurationService = carrierConfigurationService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarrierConfigurations()
        {
            var configurations = await _mediator.Send(new GetCarrierConfigurationsQuery());
            return Ok(configurations);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarrierConfiguration([FromBody] CreateCarrierConfigurationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok($"{result} ID’li kargo konfigürasyonu başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrierConfiguration(int id, [FromBody] UpdateCarrierConfigurationCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID uyuşmazlığı!");

            var result = await _mediator.Send(command);
            return Ok($"{result} ID’li kargo konfigürasyonu başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrierConfiguration(int id)
        {
            var result = await _mediator.Send(new DeleteCarrierConfigurationCommand(id));
            return Ok($"{result} ID’li kargo konfigürasyonu başarıyla silindi.");
        }
    }
}

