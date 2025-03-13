using Application.Concrete;
using Application.Orders.Command;
using Application.Orders.Query;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IMediator _mediator;

        public OrdersController(OrderService orderService, IMediator mediator)
        {
            _orderService = orderService;
            _mediator = mediator;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        //{
        //    return Ok(await _orderService.GetAllOrdersAsync());
        //}

        //[HttpPost("add")]
        //public async Task<IActionResult> AddOrder([FromBody] int orderDesi)
        //{
        //    var newOrderId = await _orderService.AddOrderAsync(orderDesi);
        //    return Ok($"{newOrderId} ID’li sipariş başarıyla eklendi.");
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<string>> DeleteOrder(int id)
        //{
        //    var newOrderId = await _orderService.DeleteOrderAsync(id);
        //    return Ok($"{newOrderId} ID’li sipariş başarıyla silindi.");
        //}

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok($"{result} ID’li sipariş başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id));
            return Ok($"{result} ID’li sipariş başarıyla silindi.");
        }
    }
}
